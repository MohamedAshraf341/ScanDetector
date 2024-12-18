using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Features.ScannerResult.Commands.Models;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.ScannerResult.Commands.Validators
{
    public class AddScannerResultValidator : AbstractValidator<AddScannerResultCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddScannerResultValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(command => command.CameraId)
               .NotNull()
               .WithMessage(_localizer[SharedResourcesKeys.Required])
               .NotEmpty()
               .WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
        }
    }
}
