using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Features.UsersRelative.Commands.Models;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Validators
{
    public class AddUsersRelativeValidator: AbstractValidator<AddUsersRelativeCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddUsersRelativeValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(command => command.Data)
               .NotNull()
               .WithMessage(_localizer[SharedResourcesKeys.Required])
               .Must(settings => settings.Any())
               .WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
    }
}
