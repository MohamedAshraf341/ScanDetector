using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Features.User.Queries.Models;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.User.Queries.Validators
{
    public class GetUserByIdValidator: AbstractValidator<GetUserByIdQuery>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetUserByIdValidator(IStringLocalizer<SharedResources> localizer) 
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
