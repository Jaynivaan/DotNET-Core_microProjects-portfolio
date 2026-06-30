//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record ParticipationMetrics(
        int TotalParticipations,
        double AverageReinforcementCount,
        int HighestReinforcementCount
        );
}