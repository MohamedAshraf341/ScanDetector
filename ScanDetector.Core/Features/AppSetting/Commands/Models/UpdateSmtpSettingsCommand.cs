using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.AppSetting.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.AppSetting.Commands.Models
{
    public class UpdateSmtpSettingsCommand:IRequest<BaseResponse<List<UserSettings>>>
    {
        public List<UserSettings> SmtpSettings { get; set; }
    }
}
