//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed record HyperVectorPayload(
        Guid AttentionBallId,
        float[] Values,
        int Dimensions,
        SemanticFingerprint Fingerprint,
        DateTimeOffset CreatedAt
    );
}