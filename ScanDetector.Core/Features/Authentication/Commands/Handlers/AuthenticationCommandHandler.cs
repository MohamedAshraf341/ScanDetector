using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Models;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using ScanDetector.Data.AppMetaData;
using ScanDetector.Data.Entities;
using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<LoginCommand, BaseResponse<TokenResponse>>,
        IRequestHandler<SignUpCommand, BaseResponse<TokenResponse>>,
        IRequestHandler<SendCodeResetPasswordCommand, BaseResponse<string>>,
        IRequestHandler<ResetPasswordCommand, BaseResponse<TokenResponse>>,
        IRequestHandler<RefreshTokenCommand, BaseResponse<TokenResponse>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jWTService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IJWTService jWTService, IMapper mapper, IEmailService emailService):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _jWTService = jWTService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<BaseResponse<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email.ToLower());
            if (user == null)
                return BadRequest<TokenResponse>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.HashPassword))
                return BadRequest<TokenResponse>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var res = _mapper.Map<TokenResponse>(user);

            var jwtSecurityToken = _jWTService.CreateJwtToken(user, res.Permissions);
            res.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            res.TokenExpiresOn = jwtSecurityToken.ValidTo;
            res.Role = user.Role.NameEn;
            if (user.RefreshTokens.Count == 0 || !user.RefreshTokens.Any(t => t.IsActive))
            {
                var refreshToken = _jWTService.GenerateRefreshToken();
                res.RefreshToken = refreshToken.Token;
                res.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                _unitOfWork.Complete();
            }
            else
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                res.RefreshToken = activeRefreshToken.Token;
                res.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            return Success(res);
        }



        public async Task<BaseResponse<TokenResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Data.Entities.Authentication.User>(request);
            user.RoleId = RoleData.User.Id;
            user.Permissions = PermissionData.allPermission.Select(x => new UserPermission
            {
                IsSelected = false,
                PermissionId = x.Id,
            }).ToList();

            user =await _unitOfWork.Users.AddAsync(user);
            _unitOfWork.Complete();
            var userRegister = await _unitOfWork.Users.GetByEmail(user.Email);
            var res = _mapper.Map<TokenResponse>(userRegister);
            var jwtSecurityToken = _jWTService.CreateJwtToken(userRegister, res.Permissions);
            res.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            res.TokenExpiresOn = jwtSecurityToken.ValidTo;
            res.Role = userRegister.Role.NameEn;
            var refreshToken = _jWTService.GenerateRefreshToken();
            res.RefreshToken = refreshToken.Token;
            res.RefreshTokenExpiration = refreshToken.ExpiresOn;
            refreshToken.UserId = userRegister.Id;
            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            _unitOfWork.Complete();
            return Created(res);
        }

        public async Task<BaseResponse<string>> Handle(SendCodeResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user= await _unitOfWork.Users.GetByEmail(request.Email);
            if(user == null)
                return NotFound<string>();
            var chars = "0123456789";
            var random = new Random();
            var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            var code= new UserCode { Code = randomNumber,UserId=user.Id,ExpiresOn= DateTime.Now.AddHours(1) };
            await _unitOfWork.UserCodes.AddAsync(code);
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\ResetPasswordTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText
                .Replace("[code]", randomNumber);

            await _emailService.SendEmailFromSystemAsync(request.Email, "Reset Password Code", mailText);
            _unitOfWork.Complete();
            return Success(randomNumber);
        }

        public async Task<BaseResponse<TokenResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email);
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _unitOfWork.Users.Update(user);
             _unitOfWork.Complete();
            var res = _mapper.Map<TokenResponse>(user);

            var jwtSecurityToken = _jWTService.CreateJwtToken(user,res.Permissions);
            res.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            res.TokenExpiresOn = jwtSecurityToken.ValidTo;
            res.Role = user.Role.NameEn;
            if (user.RefreshTokens.Count == 0 || !user.RefreshTokens.Any(t => t.IsActive))
            {
                var refreshToken = _jWTService.GenerateRefreshToken();
                res.RefreshToken = refreshToken.Token;
                res.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                 _unitOfWork.Complete();
            }
            else
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                res.RefreshToken = activeRefreshToken.Token;
                res.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            return Success(res);
        }

        public async Task<BaseResponse<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user=await _unitOfWork.Users.GetByRefreshToken(request.RefreshToken);
            if (user == null)
                return Unauthorized< TokenResponse>();
            var refreshToken = user.RefreshTokens.Single(t => t.Token == request.RefreshToken);
            if(refreshToken == null)
                return NotFound< TokenResponse>();
            if (!refreshToken.IsActive)
                return Unauthorized<TokenResponse>("Refresh token not active");
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = _jWTService.GenerateRefreshToken();
            newRefreshToken.UserId = user.Id;
            await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
             _unitOfWork.Complete();
            var res = _mapper.Map<TokenResponse>(user);

            var jwtToken = _jWTService.CreateJwtToken(user, res.Permissions);

            res.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            res.TokenExpiresOn = jwtToken.ValidTo;
            res.RefreshToken = newRefreshToken.Token;
            res.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            res.Role = user.Role.NameEn;
            return Success(res);
        }
    }
}
