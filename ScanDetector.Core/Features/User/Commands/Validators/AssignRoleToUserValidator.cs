

using ScanDetector.Core.Features.User.Commands.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.User.Commands.Validators
{
    public class AssignRoleToUserValidator: AbstractValidator<AssignRoleToUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AssignRoleToUserValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.UserId)
               .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Users.GetByIdAsync(Key) != null)
               .WithMessage("not Found.");
            RuleFor(x => x.RoleId)
               .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Roles.GetByIdAsync(Key) != null)
               .WithMessage("not Found.");
        }
    }
}
