//gs

namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct RuntimeStatistics(
        long TotalProcessedSignals,
        long TotalAcceptedSignals,
        long TotalRejectedSignals,
        long TotalCrystallizations,
        double AverageResonance,
        double AverageProcessingDurationMs,
        double SlotUtilizationPercentage);
}