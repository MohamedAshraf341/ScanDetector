using ScanDetector.Core.Features.User.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.User.Commands.Validators
{
    public class DeleteUserValidator: AbstractValidator<DeleteUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteUserValidator(IStringLocalizer<SharedResources> localizer) 
        {
            _localizer = localizer;
            ApplyCustomValidationsRules();
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
    }
}
