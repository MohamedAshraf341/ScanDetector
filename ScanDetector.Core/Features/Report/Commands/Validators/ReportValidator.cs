

using FluentValidation;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Features.Report.Commands.Models;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Report.Commands.Validators
{
    public class ReportValidator: AbstractValidator<ReportRequest>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ReportValidator(IStringLocalizer<SharedResources> localizer) 
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            // Title is required and must not be empty
            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            // CultureCode is required and must be a valid format
            RuleFor(request => request.CultureCode)
                .NotEmpty().WithMessage("Culture code is required.")
                .Matches(@"^[a-z]{2}-[A-Z]{2}$").WithMessage("Culture code must be in the format 'xx-XX' (e.g., 'en-US').");

            // Headers should not be empty and must have keys and values
            RuleFor(request => request.Headers)
                .NotEmpty().WithMessage("Headers are required.")
                .Must(headers => headers.Values.All(v => !string.IsNullOrWhiteSpace(v)))
                .WithMessage("All header values must be non-empty.");

            // Items should not be empty
            RuleFor(request => request.Items)
                .NotEmpty().WithMessage("Items are required.")
                .Must(items => items.All(item => item != null))
                .WithMessage("Items must not contain null values.");
        }
    }
}
