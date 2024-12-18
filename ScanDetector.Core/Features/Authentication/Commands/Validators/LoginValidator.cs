﻿using ScanDetector.Core.Features.Authentication.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Commands.Validators
{
    public class LoginValidator: AbstractValidator<LoginCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public LoginValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
    }
}
