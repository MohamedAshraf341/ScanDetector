using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using ScanDetector.Core.Features.UserProfile.Commands.Models;
using ScanDetector.Data.Entities;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UserProfile.Commands.Handlers
{
    internal class UserProfileCommandHandler: ResponseHandler,
                IRequestHandler<LogoutCommand, BaseResponse<string>>,
        IRequestHandler<UpdateProfileCommand, BaseResponse<string>>,
        IRequestHandler<ChangePhotoProfileCommand, BaseResponse<string>>,
        IRequestHandler<ChangePasswordCommand,BaseResponse<string>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UserProfileCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> stringLocalizer,
            ICurrentUserService currentUserService,
            IMapper mapper,
            IFileService fileService):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<BaseResponse<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByRefreshToken(request.RefreshToken);
            if (user == null)
                return BadRequest<string>();
            var refreshToken = await _unitOfWork.RefreshTokens.GetByRefreshToken(request.RefreshToken);
            if (refreshToken == null)
                return BadRequest<string>();
            if (!refreshToken.IsActive)
                return Unauthorized<string>("refreshToken not active.");

            refreshToken.RevokedOn = DateTime.UtcNow;
            _unitOfWork.RefreshTokens.Update(refreshToken);
             _unitOfWork.Complete();
            return Success<string>(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var currentuser = await _currentUserService.GetUserAsync();
            currentuser.FirstName = request.FirstName;
            currentuser.LastName = request.LastName;
            _unitOfWork.Users.Update(currentuser);
             _unitOfWork.Complete();  
            return Updated(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(ChangePhotoProfileCommand request, CancellationToken cancellationToken)
        {                
            var currentUser = await _currentUserService.GetUserAsync();
            if (request.IsRemove.HasValue && request.IsRemove.Value)
            {
                _fileService.DeleteImage(currentUser.PictureUrl);
                currentUser.PictureUrl= null;
                _unitOfWork.Users.Update(currentUser);
                 _unitOfWork.Complete();
                return Deleted<string>();
            }
            if(request.File!=null)
            {
                var newUrlImage = await _fileService.UploadImage("UserImage",request.File);
                currentUser.PictureUrl= newUrlImage;
                _unitOfWork.Users.Update(currentUser);
                 _unitOfWork.Complete();
                return Created(string.Empty);
            }
            return BadRequest<string>();
        }

        public async Task<BaseResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUser=await _currentUserService.GetUserAsync();
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, currentUser.HashPassword))
                return BadRequest<string>("Old Password Invalid");
            currentUser.HashPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _unitOfWork.Users.Update(currentUser);
             _unitOfWork.Complete();
            return Updated(string.Empty);
        }
    }
}
