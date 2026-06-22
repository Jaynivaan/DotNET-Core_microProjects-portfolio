//gs

using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IHyperVectorEncoder
    {
        HyperVectorPayload Encode(
            Guid attentionBallId,
            VectorPreparationResult vectorPreparation);
    }
}