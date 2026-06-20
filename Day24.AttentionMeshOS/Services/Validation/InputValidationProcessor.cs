//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace  Day24.AttentionMeshOS.Services
{
    public sealed class InputValidationProcessor : IInputProcessor
    {
        private readonly ILogger<InputValidationProcessor> _logger;
        private readonly AttentionInputValidationOptions _options;
        private readonly RawAttentionInputValidator _validator;

        public int ExecutionOrder => 1;

        public bool IsCritical => true;

        public InputValidationProcessor(
            ILogger<InputValidationProcessor> logger,
            IOptions<AttentionInputValidationOptions> options,
            RawAttentionInputValidator validator
            )
        {
            _logger = logger;

            _options = options.Value; 

            _validator = validator;
        }

        public async Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default
            )
        {
            if (!_options.EnableValidation)
            {
                _logger.LogInformation(
                    "Input validation skipped for RawInput {RawInputId}.",
                    context.RawInput.Id);

                return ProcessorControl.Continue;
            }

            var validationResult = await _validator.ValidateAsync(
                context.RawInput,
                cancellationToken
                );

            foreach (var error in validationResult.Errors)
            {
                context.ValidationResult.Errors.Add(
                    error.ErrorMessage);                    
            }
            if (context.ValidationResult.IsValid)
            {
                _logger.LogInformation(
                    "RawInput {RawInputId} passed validation.",
                    context.RawInput.Id
                    );
                return ProcessorControl.Continue;
            }          

            else
            {
                _logger.LogWarning(
                    "RawInput {RawInputId} failed validation with {ErrorCount} errors.",
                    context.RawInput.Id,
                    context.ValidationResult.Errors.Count);

                return ProcessorControl.ShortCircuit;
            }
        }
    }
}