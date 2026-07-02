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
        double AverageMembershipsPerDynamicTag,

        //Physics Metrics
        float AverageAttentionEnergy,
        float AverageAttractionPotential,
        float AverageSemanticMomentum,
        Guid? HighestPotentialFieldId,
        Guid? FastestRisingFieldId,
        Guid? WeakestActiveFieldId,
        float HighestEnergyObserved,
        float HighestPotentialObserved,
        float HighestMomentumObserved
        );
}