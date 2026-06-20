//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class TagExtractionProcessor : IInputProcessor
    {
        private readonly ILogger<TagExtractionProcessor> _logger;
        private readonly TagExtractionOptions _options;
        private readonly ITagRuleProvider _tagRuleProvider;

        public int ExecutionOrder => 5;

        public bool IsCritical => false;


        public TagExtractionProcessor(
            ILogger<TagExtractionProcessor> logger,
            IOptions<TagExtractionOptions> options,
            ITagRuleProvider tagRuleProvider
            )
        {
            _logger = logger;
            _options = options.Value;
            _tagRuleProvider = tagRuleProvider;

        }

        public Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default
            )
        {
            if (!_options.Enabled)
            {
                _logger.LogInformation(
                    "Tag Extraction Processor skipped by configuration for the raw input {RawInputId}.",
                    context.RawInput.Id);
                return Task.FromResult(ProcessorControl.Continue);
            }

            var keywords = context.KeywordExtractionResult?.Keywords
                ?? [];

            if (keywords.Count == 0)
            {
                _logger.LogInformation(
                    "Tag Extraction processor found no keywords for RawInput {RawInputId}.",
                    context.RawInput.Id);

                return Task.FromResult(ProcessorControl.Continue);

            }

            var mappings = _tagRuleProvider.GetMappings();

            var tags = keywords
                .Where(keywords => mappings.ContainsKey(keywords))
                .SelectMany(keywords => mappings[keywords])
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            context.TagExtractionResult =
                new TagExtractionResult(tags);

            _logger.LogInformation(
                "TagExtractionProcessor Extracted {TagCount} tags for RawInput {RawInputId}: {tags}",
                tags.Count,
                context.RawInput.Id,
                string.Join(",", tags));

            return Task.FromResult(ProcessorControl.Continue);
        }
    }
}