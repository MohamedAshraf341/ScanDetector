using ScanDetector.Core.Features.UserProfile.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UserProfile.Commands.Validators
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public LogoutValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer=localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RefreshToken)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
    }
}
