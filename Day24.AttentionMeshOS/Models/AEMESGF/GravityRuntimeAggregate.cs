//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityRuntimeAggregate(
        int Count,
        float TotalMass, 
        int ActiveMemberships,
        int LargestFieldCount,
        Guid? NewestFieldId,
        Guid? StrongestFieldId,
        float AverageStability,
        float AverageRadius,
        double AverageMembershipsPerDynamicTag
        );
}