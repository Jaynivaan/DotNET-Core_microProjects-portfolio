//gs
namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct RuntimeHealth(
        string Name,
        string Version,
        RuntimeHealthStatus Status,
        bool Initialized,
        bool RegistryAvailable,
        bool SnapshotProviderAvailable,
        TimeSpan Uptime
        );
}