//gs

namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct GravityRuntimeStatistics(
        int FieldsCreated,
        int FieldsMerged,
        int FieldsDissolved,
        double AverageFieldSize,
        double AverageSemanticMass,
        double MaximumObservedDensity,
        double AverageMembershipsPerDynamicTag,

        //physics statistics
        int PhysicsEvaluations,
        float HighestEnergyObserved,
        float HighestPotentialObserved,
        float HighestMomentumObserved,
        float AverageAttentionEnergy,
        float AveragePotential,
        float AverageMomentum

        );
}