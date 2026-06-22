//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionBallMetadataFactory : IAttentionBallMetadataFactory
    {
        private readonly ILogger<AttentionBallMetadataFactory> _logger;
        private readonly IHyperVectorEncoder _hyperVectorEncoder;

        public AttentionBallMetadataFactory(
            ILogger<AttentionBallMetadataFactory> logger,
            IHyperVectorEncoder hyperVectorEncoder)
        {
            _logger = logger;
            _hyperVectorEncoder = hyperVectorEncoder;

        }

        public AttentionBallMetadata Create( 
            AttentionBall attentionBall,
            InputProcessingContext inputContext)
        {
            if (inputContext.VectorPreparationResult is null)
            {
                throw new InvalidOperationException(
                    "VectorPreparationResult was not found in Input processing context.");
            }

            var hyperVector = _hyperVectorEncoder.Encode(
                attentionBall.Id,
                inputContext.VectorPreparationResult);

            var metadata = new AttentionBallMetadata(
                attentionBall.Id,
                inputContext.VectorPreparationResult,
                hyperVector
                );
            _logger.LogInformation(
                "Metadata created for AttentionBall {AttentionBallid}.",
                attentionBall.Id);
            
            return metadata;
        }
    }
}