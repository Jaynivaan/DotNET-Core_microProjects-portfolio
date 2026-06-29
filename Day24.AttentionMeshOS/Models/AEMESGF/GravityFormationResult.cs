//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFormationResult(
        bool WasProcessed,
        bool FieldCreated,
        bool FieldMatched,
        Guid? GravityFieldId,
        float ProximityScore,
        GravityFieldLifecycleState LifecycleState
        );
}