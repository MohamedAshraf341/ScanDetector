using ScanDetector.Core.Features.UserProfile.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UserProfile.Commands.Validators
{
    public class UpdateProfileValidator: AbstractValidator<UpdateProfileCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateProfileValidator(IStringLocalizer<SharedResources> localizer) 
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FirstName)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.LastName)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
    }
}
