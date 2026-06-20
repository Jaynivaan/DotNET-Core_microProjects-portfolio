//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class NoiseReductionProcessor : IInputProcessor
    {
        private readonly ILogger<NoiseReductionProcessor> _logger;
        private readonly NoiseReductionOptions _options;

        public int ExecutionOrder => 3;

        public bool IsCritical => false;

        public NoiseReductionProcessor(
            ILogger<NoiseReductionProcessor> logger,
            IOptions<NoiseReductionOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;
        }

        public Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default
            )
        {
            if (!_options.Enabled)
            {
                _logger.LogInformation(
                    "Noise Reduction processor skipped by configuration for the RawInput {RawInputId}.",
                    context.RawInput.Id
                    );
                return Task.FromResult(ProcessorControl.Continue);
            }

            var inputText =
                context.NormalizedInput?.NormalizedText
                ?? context.RawInput.Text;

            var structurallyCleanedText = Regex.Replace(
                inputText,
                @"[^\w\s]",
                " "
                );

            var words = structurallyCleanedText
                .Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
                );

            var lightNoiseWords = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase
                )
            {
                "um","umm", "ummm", "uh", "hmm", "ha" , "hah", "lol"
            };

            var mediumNoiseWords = new HashSet<string>(
                lightNoiseWords,
                StringComparer.OrdinalIgnoreCase
                )
            {
                "like", "actually", "basically", "just","really", "very", "maybe"
            };

            var aggressiveNoiseWords = new HashSet<string>(
                mediumNoiseWords,
                StringComparer.OrdinalIgnoreCase)
            {
                "the", "a", "an", "and", "or", "but", "is", "are", "was", "were",
                "to", "of", "in", "on", "for", "with", "this", "that", "it"
            };

            var selectedNoiseWords = _options.Level switch
            {
                "Light" => lightNoiseWords,
                "Medium" => mediumNoiseWords,
                "Aggressive" => aggressiveNoiseWords,
                _ => lightNoiseWords
            };
            var reducedText = string.Join(
                " ",
                words.Where(word => !selectedNoiseWords.Contains(word))
                );
            

            
            while (reducedText.Contains("  "))
            {
                reducedText = reducedText.Replace("  ", " ");
            }

            context.NoiseReducedInput = new NoiseReducedInput(
                inputText,
                reducedText
                );

            _logger.LogInformation(
                """
                Noise Reduction Processor executed for RawInput {RawInputId}. 
                Level : {Level}
                Original: [{original}]
                Reduced : [{Reduced}]              
                
                """,
                context.RawInput.Id,
                _options.Level,
                inputText,
                reducedText
                );

            return Task.FromResult(ProcessorControl.Continue);

        }
    }
}