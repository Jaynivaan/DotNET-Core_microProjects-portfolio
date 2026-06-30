//gs
namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct GravityRuntimeSnapshot(
        int GravityFieldCount,
        float TotalSemanticMass,
        int ActiveMemberships,
        int LargestField,
        Guid? NewestFeildId,
        Guid? StrongestFeildId,
        float AverageStability,
        float AverageFieldRadius
        );
}