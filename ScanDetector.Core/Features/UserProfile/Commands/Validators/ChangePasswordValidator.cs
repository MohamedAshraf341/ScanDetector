using ScanDetector.Core.Features.UserProfile.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UserProfile.Commands.Validators
{
    public class ChangePasswordValidator: AbstractValidator<ChangePasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ChangePasswordValidator(IStringLocalizer<SharedResources> localizer) 
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.OldPassword)
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
