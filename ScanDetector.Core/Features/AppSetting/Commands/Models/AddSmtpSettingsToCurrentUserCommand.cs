using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.AppSetting.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.AppSetting.Commands.Models
{
    public class AddSmtpSettingsToCurrentUserCommand:IRequest<BaseResponse<List<UserSettings>>>
    {
    }
}
