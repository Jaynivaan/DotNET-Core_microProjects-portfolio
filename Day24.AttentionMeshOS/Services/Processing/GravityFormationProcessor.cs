//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFormationProcessor : IInputProcessor
    {
        private readonly IDynamicTagRegistry _dynamicTagRegistry;
        private readonly IGravityFormationEngine _gravityFormationEngine;
        private readonly ILogger<GravityFormationProcessor> _logger;

        public int ExecutionOrder => 81;

        public bool IsCritical => true;

        public GravityFormationProcessor(
            IDynamicTagRegistry dynamicTagRegistry,
            IGravityFormationEngine gravityFormationEngine,
            ILogger<GravityFormationProcessor> logger)
        {
            _dynamicTagRegistry = dynamicTagRegistry;
            _gravityFormationEngine = gravityFormationEngine;
            _logger = logger;
        }

        public Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default)
        {
            var crystallizationResult = context.CrystallizationResult;

            _logger.LogInformation(
                "AEMESGF reached. HasResult={HasResult}, WasCrystallized={WasCrystallized}, TagName={TagName}",
                crystallizationResult is not null,
                crystallizationResult?.WasCrystallized,
                crystallizationResult?.CrystallizedTagName);

            if ( crystallizationResult is null ||
                !crystallizationResult.WasCrystallized ||
                string.IsNullOrWhiteSpace(crystallizationResult.CrystallizedTagName))
            {
                return Task.FromResult(ProcessorControl.Continue);
            }

            string tagName = crystallizationResult.CrystallizedTagName;

            DynamicTagBirth? birth =
                _dynamicTagRegistry.Get(tagName);

            _logger.LogWarning(
                    "AEM-ESGF registry lookup. TagName: {TagName},  Found={Found}. ",
                    tagName,
                    birth is not null);

            if (birth is null)
            {
                _logger.LogWarning(
                    "AEM-ESGF skipped. Tagbirth not found: {TagName}",
                    tagName);

                return Task.FromResult(ProcessorControl.Continue);
            }
            

            var formationContext = new GravityFormationContext(
                DynamicTagId: birth.Id,
                DisplayName: birth.Name,
                TernarySignature: birth.TernarySignature,
                PresenceMask: BuildPresenceMask(birth.TernarySignature),
                SignalVocabulary: BuildSignalVocabulary(birth.Name),
                ObservedAt: birth.BornAt);

            _logger.LogInformation("Before _GravityFormationEngine.Process(formationContext ); inside formationProcessor.");


            GravityFormationResult result = _gravityFormationEngine.Process(formationContext);

          _logger.LogInformation(
                "After _GravityFormationEngine.Process(formationContext ); inside formationProcessor. Processed={Processed}, Created={Created}, Matched={Matched}",
                result.WasProcessed,
                result.FieldCreated,
                result.FieldMatched);

            if (result.FieldCreated)
            {                
                AemEsgfTelemetry.GravityFieldCreated(
                    _logger,
                    result.GravityFieldId!.Value);
            }
            else if ( result.FieldMatched)
            {

                AemEsgfTelemetry.GravityFieldMatched(
                    _logger,
                    result.GravityFieldId!.Value);
            }

            _logger.LogInformation(
                "Gravity formation Processor completed");

            return Task.FromResult(ProcessorControl.Continue);
        }
        private static sbyte[] BuildPresenceMask(sbyte[] signature)
        {
            var mask = new sbyte[signature.Length];

            for (int i = 0; i < signature.Length; i++)
            {
                mask[i] = signature[i] == 0
                    ? (sbyte)0
                    : (sbyte)1;
            }

            return mask;
        }

        private static IReadOnlyDictionary<string, int> BuildSignalVocabulary(
            string name)
        {
            var vocabulary = new Dictionary<string, int>(
                StringComparer.Ordinal);

            string[] parts = name.Split(
                '-',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries);
            
            for ( int i = 0; i < parts.Length; i++ )
            {
                if (vocabulary.TryGetValue(parts[i], out int count))
                {
                    vocabulary[parts[i]] = count + 1;
                }
                else
                {
                    vocabulary.Add(parts[i], 1);
                }
            }

            return vocabulary;
        }
    }
}