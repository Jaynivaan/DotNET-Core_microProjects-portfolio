//gs
using System.Text.Json;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class TagRuleProvider : ITagRuleProvider
    {
        private readonly ILogger<TagRuleProvider> _logger;
        private readonly IReadOnlyDictionary<string, IReadOnlyList<string>> _mappings;

        public TagRuleProvider(
            ILogger<TagRuleProvider> logger,
            IOptions<TagExtractionOptions> options
            )
        {
            _logger = logger;
            
            var rulesFilePath = options.Value.RulesFilePath;

            if (!File.Exists( rulesFilePath ))
            {
                _logger.LogWarning(
                    "Tag Rules file not found at {RulesFilePath}.Empty tag mapping will be used.",
                    rulesFilePath);

                _mappings = new Dictionary<string, IReadOnlyList<string>>();
                return;

            }

            var json = File.ReadAllText( rulesFilePath );

            _mappings =
                JsonSerializer.Deserialize <Dictionary<string, IReadOnlyList<string>>>(json)
                ?? new Dictionary<string, IReadOnlyList<string>>();

            _logger.LogInformation(
                "Loaded {TagRuleCount} tag rule mappings from {RulesFilePath}.",
                _mappings.Count,
                rulesFilePath);
        }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> GetMappings()
            { return _mappings; }

        
    }
}
