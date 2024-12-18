using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Extention;
using ScanDetector.Core.Features.User.Commands.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using MediatR;
using ScanDetector.Data.Entities.Authentication;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;
using ScanDetector.Data.AppMetaData;

namespace ScanDetector.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, BaseResponse<string>>,
        IRequestHandler<SimpleAddUserCommand, BaseResponse<string>>,
        IRequestHandler<UpdateUserCommand, BaseResponse<string>>,
        IRequestHandler<DeleteUserCommand, BaseResponse<string>>,
        IRequestHandler<AssignRoleToUserCommand, BaseResponse<string>>,
        IRequestHandler<ChangePasswordCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<BaseResponse<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
           var user= _mapper.Map<Data.Entities.Authentication.User>(request);
            var password = PasswordGenerator.GeneratePassword();
            user.HashPassword= BCrypt.Net.BCrypt.HashPassword(password);
            user.Permissions = PermissionData.allPermission.Select(x => new UserPermission
            {
                IsSelected = false,
                PermissionId = x.Id,
            }).ToList();
            await _unitOfWork.Users.AddAsync(user);
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\WelcomeTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText
                .Replace("[name]", $"{request.FirstName} {request.LastName}")
                .Replace("[email]", request.Email)
                .Replace("[password]", password);

            await _emailService.SendEmailAsync(request.Email, "Welcome Email", mailText);
             _unitOfWork.Complete();
            return Created(string.Empty);

        }

        public async Task<BaseResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>();
            _unitOfWork.Users.Delete(user);
             _unitOfWork.Complete();
            return Deleted<string>();
        }

        public async Task<BaseResponse<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            user.RoleId= request.RoleId;
            _unitOfWork.Users.Update(user);
             _unitOfWork.Complete();
            return Updated(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(SimpleAddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Data.Entities.Authentication.User>(request);
            user.Permissions = PermissionData.allPermission.Select(x => new UserPermission
            {
                IsSelected = false,
                PermissionId = x.Id,
            }).ToList();
            await _unitOfWork.Users.AddAsync(user);
            _unitOfWork.Complete();
            return Created(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByID(request.Id);
            if (user == null)
                return NotFound<string>();
            var checkByEmailAndId = await _unitOfWork.Users.GetByEmailNotId(request.Email, request.Id);
            if(checkByEmailAndId != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);

            user.Email=request.Email;
            user.FirstName=request.FirstName;
            user.LastName=request.LastName;
            user.RoleId=request.RoleId;
            MapPermission.UpdateIsSelectedProperty(request.Permissions, user.Permissions);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
            return Updated(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Users.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound<string>();
            item.HashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _unitOfWork.Users.Update(item);
            _unitOfWork.Complete();
            return Updated(string.Empty);
        }
    }
}
