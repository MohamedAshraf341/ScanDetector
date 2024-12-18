using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.AppSetting.Commands.Models
{
    public class DeleteSmtpSettingsToCurrentUserCommand:IRequest<BaseResponse<string>>
    {
    }
}
