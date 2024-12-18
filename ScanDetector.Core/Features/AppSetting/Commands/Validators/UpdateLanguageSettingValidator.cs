using ScanDetector.Core.Features.AppSetting.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.AppSetting.Commands.Validators
{
    public class UpdateLanguageSettingValidator : AbstractValidator<UpdateLanguageSettingCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateLanguageSettingValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();

        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Value)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
    }
}
