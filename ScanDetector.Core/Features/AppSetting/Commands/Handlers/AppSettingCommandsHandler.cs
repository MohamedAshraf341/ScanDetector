using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Extention;
using ScanDetector.Core.Features.AppSetting.Commands.Models;
using ScanDetector.Core.Features.AppSetting.Commands.Results;
using ScanDetector.Data.AppMetaData;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.AppSetting.Commands.Handlers
{
    public class AppSettingCommandsHandler : ResponseHandler,
        IRequestHandler<AddSmtpSettingsToCurrentUserCommand, BaseResponse<List<UserSettings>>>,
        IRequestHandler<DeleteSmtpSettingsToCurrentUserCommand, BaseResponse<string>>,
        IRequestHandler<UpdateSmtpSettingsCommand, BaseResponse<List<UserSettings>>>,
        IRequestHandler<GetLanguageUserCommand, BaseResponse<UserSettings>>,
        IRequestHandler<UpdateLanguageSettingCommand, BaseResponse<UserSettings>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public AppSettingCommandsHandler(IStringLocalizer<SharedResources> stringLocalizer,IUnitOfWork unitOfWork, ICurrentUserService currentUserService,IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<UserSettings>>> Handle(AddSmtpSettingsToCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var items = await _unitOfWork.AppSetting.GetSmtpSettingsByUserId(currentUserId);
            var listItems=new List<Data.Entities.Authentication.AppSetting>();
            if (!items.Any())
            {
                foreach (var setting in AppSettingData.all)
                {
                    setting.UserId = currentUserId;
                    listItems.Add(setting);
                }
            }
            if(listItems.Any())
            {
                await _unitOfWork.AppSetting.AddRange(listItems);
                 _unitOfWork.Complete();
                items = await _unitOfWork.AppSetting.GetSmtpSettingsByUserId(currentUserId);
            }

            var userSettingsList = items.ToUserSettingsList();
            return Success(userSettingsList);
        }

        public async Task<BaseResponse<string>> Handle(DeleteSmtpSettingsToCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var items = await _unitOfWork.AppSetting.GetSmtpSettingsByUserId(currentUserId);
            if (!items.Any())
                return NotFound<string>();

            _unitOfWork.AppSetting.DeleteRange(items);
             _unitOfWork.Complete();
            return Deleted<string>();
        }

        public async Task<BaseResponse<List<UserSettings>>> Handle(UpdateSmtpSettingsCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            foreach (var item in request.SmtpSettings)
            {
                var dbItem=await _unitOfWork.AppSetting.GetByIdAsync(item.Id);
                if (dbItem != null)
                {
                    dbItem.Value = item.Value.ToString();
                    _unitOfWork.AppSetting.Update(dbItem);
                }
            }
             var res=_unitOfWork.Complete();
            var Newitems = await _unitOfWork.AppSetting.GetSmtpSettingsByUserId(currentUserId);
            var userSettingsList = Newitems.ToUserSettingsList();
            return Updated(userSettingsList);
        }

        public async Task<BaseResponse<UserSettings>> Handle(GetLanguageUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var item = await _unitOfWork.AppSetting.GetByUserIdAndName(userId, AppSettingData.Language.Name);
            if (item == null) 
            {
                item = AppSettingData.Language;
                item.UserId = userId;
                item.Value = "en-US";
                item =await _unitOfWork.AppSetting.AddAsync(item);
                _unitOfWork.Complete();
            }
            var res = item.ToUserSettings();
            return Success(res);
        }

        public async Task<BaseResponse<UserSettings>> Handle(UpdateLanguageSettingCommand request, CancellationToken cancellationToken)
        {
            var userId=_currentUserService.GetUserId();
            var language=await _unitOfWork.AppSetting.GetByUserIdAndName(userId, AppSettingData.Language.Name);
            if (language == null)
                return NotFound<UserSettings>();
            language.Value=request.Value;
            var item=_unitOfWork.AppSetting.Update(language);
            _unitOfWork.Complete();
            var res = item.ToUserSettings();
            return Updated(res);
        }
    }
}
