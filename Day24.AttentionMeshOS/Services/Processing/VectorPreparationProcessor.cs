//gs
using System.Text;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class VectorPreparationProcessor : IInputProcessor
    {
        private readonly ILogger<VectorPreparationProcessor> _logger;
        private readonly VectorPreparationOptions _options;

        public int ExecutionOrder => 6;

        public bool IsCritical => false;

        public VectorPreparationProcessor(
            ILogger<VectorPreparationProcessor> logger,
            IOptions<VectorPreparationOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;

        }

        public Task<ProcessorControl>ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default
            )
        {
            if (!_options .Enabled)
            {
                _logger.LogInformation(
                    "Vector Preparation Processing skipped by the configuration for the RawInput {RawInputId}>",
                    context.RawInput.Id );

                return Task .FromResult(ProcessorControl.Continue);
            }

            var keywords = context.KeywordExtractionResult? .Keywords
                ?? Array.Empty<string>();

            var tags = context.TagExtractionResult?.Tags
                ?? Array .Empty<string>();

            var uniqueKeywords = new HashSet<string>(
                keywords,
                StringComparer.OrdinalIgnoreCase
                );

            var uniqueTags = new HashSet<string>(
                tags,
                StringComparer.OrdinalIgnoreCase
                );

            var builder = new StringBuilder();

            builder.AppendLine("Text");
            builder.AppendLine(context.EffectiveText);

            if (uniqueKeywords.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("Keywords:");
                builder.AppendLine(string.Join(", ", uniqueKeywords));
            }

            if (uniqueTags.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("Tags :");
                builder.AppendLine(string.Join(",", uniqueTags));

            }

            context.VectorPreparationResult =
                new VectorPreparationResult(
                    builder.ToString().Trim(),
                    uniqueKeywords.ToList(),
                    uniqueTags.ToList(),
                    DateTimeOffset.UtcNow
                    );

            _logger.LogInformation(
                "Vector Preparation Processor prepared vector payload  text  for RawInput {RawInputId}. Keywords = {KeywordsCount}, Tags = {TagCount}",
                context.RawInput.Id,
                uniqueKeywords.Count,
                uniqueTags.Count
                );

            return Task.FromResult(ProcessorControl.Continue);
        }
    }
}