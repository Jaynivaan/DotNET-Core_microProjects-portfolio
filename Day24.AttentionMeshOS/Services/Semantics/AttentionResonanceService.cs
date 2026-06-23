//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionResonanceService : IAttentionResonanceService
    {
        private readonly ILogger<AttentionResonanceService> _logger;
        private readonly ResonanceOptions _options;
        private readonly ITextSimilarityService _textSimilarityService;
        private readonly IAttentionBallMetadataStore _metadataStore;
        private readonly IResonanceCalculator _resonanceCalculator;

        public AttentionResonanceService(
            ITextSimilarityService textSimilarityService,
            IAttentionBallMetadataStore metadataStore,
            IResonanceCalculator resonanceCalculator,
            IOptions<ResonanceOptions> options,
            ILogger<AttentionResonanceService> logger)
        {
            _textSimilarityService = textSimilarityService;
            _metadataStore = metadataStore;
            _resonanceCalculator = resonanceCalculator;
            _options = options.Value;
            _logger = logger;
        }

        public double CalculateResonance(
            AttentionBall source,
            AttentionBall target)
        {
            var textScore = _textSimilarityService.CalculateSimilarity(
                source.CurrentAim,
                target.CurrentAim);

            var hyperVectorScore = CalculateHyperVectorScore(
                source,
                target);

            var finalScore = 
                (textScore * _options.TextWeight) + 
                (hyperVectorScore * _options.HyperVectorWeight);

            finalScore = Math.Max(0, finalScore);

            if (finalScore < _options.MinimumScore)
            {
                return 0;
            }


            _logger.LogInformation(
                "Attention resonance calculated {SourceId} -> {TargetId}. Text= {TextScore:F4}, HyperVector = {HyperVectorScore:F4}, BundledMeshECHO = {BundledMeshEcho:F4}",
                source.Id,
                target.Id,
                textScore,
                hyperVectorScore,
                finalScore);
            
            return finalScore;

        }

        private double CalculateHyperVectorScore(
            AttentionBall source,
            AttentionBall target)
        {
            if ( !_options.Enabled )
            {
                return 0;
            }

            var sourceMetadata = _metadataStore.GetByAttentionBallId(source.Id);
            var targetMetadata = _metadataStore.GetByAttentionBallId(target.Id);

            if ( sourceMetadata is null || targetMetadata is null )
            {
                return 0;
            }

            return _resonanceCalculator.Calculate(
                sourceMetadata.HyperVector,
                targetMetadata.HyperVector).ResonanceScore;
        }


    }
}