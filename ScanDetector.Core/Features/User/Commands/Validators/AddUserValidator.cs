using ScanDetector.Core.Features.User.Commands.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.User.Commands.Validators
{
    public class AddUserValidator: AbstractValidator<AddUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddUserValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) 
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRules()
        {
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

        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Email)
               .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Users.GetByEmail(Key) == null)
               .WithMessage("is already used");
            RuleFor(x => x.RoleId)
               .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Roles.GetByIdAsync(Key) != null)
               .WithMessage("not Found.");
        }
    }
}
