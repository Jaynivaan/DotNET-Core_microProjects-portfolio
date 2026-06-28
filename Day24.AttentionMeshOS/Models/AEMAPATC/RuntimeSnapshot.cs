//gs
namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct RuntimeSnapshot(
        TimeSpan Uptime,
        int TotalSlots,
        int OccupiedSlots,
        int DormantSlots,
        int ColdSlots,
        int WarmSlots,
        int HotSlots,
        int RegistrySize,
        int CrystallizationCount,
        double AverageResonance,
        double HighestEnergy,
        double AverageProcessingLatencyMs);
}