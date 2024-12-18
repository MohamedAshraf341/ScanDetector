using ScanDetector.Core.Features.Authentication.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Commands.Validators
{
    public class ResetPasswordValidator: AbstractValidator<ResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ResetPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MinimumLength(6).WithMessage("must be at least 6 characters long")
                .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("must contain at least one special character");
        }
    }
}
