

using MediatR;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.AppSetting.Commands.Results;

namespace ScanDetector.Core.Features.AppSetting.Commands.Models
{
    public class UpdateLanguageSettingCommand: IRequest<BaseResponse<UserSettings>>
    {
        public string Value { get; set; }        
    }
}
