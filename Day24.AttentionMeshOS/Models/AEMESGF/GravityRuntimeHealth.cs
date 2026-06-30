//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityRuntimeHealth(
        string Name,
        string Version,
        GravityRuntimeHealthStatus Status,
        bool RuntimeInitialized,
        bool RegistryAvailable,
        bool SnapshotProviderAvailable,
        bool StatisticsProviderAvailable,
        bool FormationEngineAvailable,
        TimeSpan Uptime
        );
}