using ScanDetector.Core.Features.AppSetting.Commands.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.AppSetting.Commands.Validators
{
    public class UpdateSmtpSettingsCommandValidator : AbstractValidator<UpdateSmtpSettingsCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateSmtpSettingsCommandValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }

        public void ApplyValidationsRules()
        {
            RuleFor(command => command.SmtpSettings)
                .NotNull()
                .WithMessage(_localizer[SharedResourcesKeys.Required])
                .Must(settings => settings.Any())
                .WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleForEach(command => command.SmtpSettings)
                .ChildRules(settings =>
                {
                    settings.RuleFor(s => s.Id)
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

                });
            RuleForEach(command => command.SmtpSettings)
                .ChildRules(settings =>
                {
                    settings.RuleFor(s => s.Name)
                    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                    .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

                });
            RuleForEach(command => command.SmtpSettings)
               .ChildRules(settings =>
               {
                   settings.RuleFor(s => s.ValueType)
                   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

               });
            RuleForEach(command => command.SmtpSettings)
               .ChildRules(settings =>
               {
                   settings.RuleFor(s => s.UserId)
                   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


               });
        }
        public void ApplyCustomValidationsRules()
        {
            RuleForEach(command => command.SmtpSettings)
                .ChildRules(settings =>
                {
                    settings.RuleFor(s => s.Id)
                    .MustAsync(async (Key, CancellationToken) => await _unitOfWork.AppSetting.GetByIdAsync(Key) != null)
                .WithMessage(_localizer[SharedResourcesKeys.NotFound]);

                });
            RuleForEach(command => command.SmtpSettings)
               .ChildRules(settings =>
               {
                   settings.RuleFor(s => s.UserId)
                   .MustAsync(async (Key, CancellationToken) => await _unitOfWork.Users.GetByIdAsync(Key.Value) != null)
                .WithMessage(_localizer[SharedResourcesKeys.NotFound]);


               });
        }
    }
}
