using ScanDetector.Core.Features.Authentication.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Commands.Validators
{
    public class RefreshTokenValidator: AbstractValidator<RefreshTokenCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public RefreshTokenValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
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
