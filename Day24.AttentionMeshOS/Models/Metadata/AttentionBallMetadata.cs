//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionBallMetadata(
        Guid AttentionBallId,
        VectorPreparationResult VectorPreparation,
        HyperVectorPayload HyperVector
    );
}