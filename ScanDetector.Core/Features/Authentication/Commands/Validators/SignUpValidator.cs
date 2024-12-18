using ScanDetector.Core.Features.Authentication.Commands.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Commands.Validators
{
    public class SignUpValidator: AbstractValidator<SignUpCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        private readonly IUnitOfWork _unitOfWork;
        public SignUpValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.CameraId)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.PhoneNumber)
               .Matches(@"^(01[0125]\d{8})$").WithMessage("Phone number must be a valid Egyptian mobile number.")
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .EmailAddress().WithMessage("address is not in a valid format. Please enter a valid email like example@domain.com");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MinimumLength(6).WithMessage("must be at least 6 characters long")
                .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("must contain at least one special character");
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Email)
                .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Users.GetByEmail(Key)==null)
                .WithMessage("is already used");
        }
    }
}
