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
        double AverageMembershipsPerDynamicTag

        );
}