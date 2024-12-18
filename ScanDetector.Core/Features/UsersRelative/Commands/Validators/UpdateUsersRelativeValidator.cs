using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Features.UsersRelative.Commands.Models;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Validators
{
    public class UpdateUsersRelativeValidator: AbstractValidator<UpdateUsersRelativeCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateUsersRelativeValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(command => command.Name)
               .NotNull()
               .WithMessage(_localizer[SharedResourcesKeys.Required])
               .NotEmpty()
               .WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
        }
    }
}
