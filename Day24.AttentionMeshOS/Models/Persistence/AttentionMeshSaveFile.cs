//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionMeshSaveFile(
        SaveMetadata Metadata,
        DynamicTagRegistryState? DynamicTags,
        GravityRegistryState? GravityRegistry,
        GravityRuntimeState? GravityRuntime,
        object? ReplayJournal
        );
}