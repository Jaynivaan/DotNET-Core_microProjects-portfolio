//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RawAttentionInputValidator : AbstractValidator<RawAttentionInput>
    {
        private readonly ILogger<RawAttentionInputValidator> _logger;

        public RawAttentionInputValidator(
            IOptions<AttentionInputValidationOptions> options,
            ILogger<RawAttentionInputValidator> logger
            )
        {
            _logger = logger;

            var validationOptions = options.Value;

            _logger.LogInformation(
                "RawAttentionInputValidator initialized. MinLength = {MinLength}, MaxLength = {MaxLength}",
                validationOptions.MinimumTextLength,
                validationOptions.MaximumTextLength);

            RuleFor(input => input.Text)
                .NotEmpty()
                .WithMessage("Input Text is required.");

            if (validationOptions.RejectWhitespacesOnly)
            {
                RuleFor(input => input.Text)
                    .Must(text => !string.IsNullOrWhiteSpace(text))
                    .WithMessage("Input text cannot be Whitespaces only.");
            }

            RuleFor(input => input.Text)
                .MinimumLength(validationOptions.MinimumTextLength)
                .WithMessage($"Input text must be at least {validationOptions.MinimumTextLength} characters.");

            RuleFor(input => input.Text)
                .MaximumLength(validationOptions.MaximumTextLength)
                .WithMessage($"Input text must not exceed  {validationOptions.MaximumTextLength} characters.");
        }
    }
}
