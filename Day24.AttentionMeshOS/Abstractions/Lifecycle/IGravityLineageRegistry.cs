//gs

using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityLineageRegistry
    {
        void RegisterBirth(Guid fieldId, DateTimeOffset createdAt);

        void RegisterMerge(
            Guid retiredFieldId,
            Guid survivorFieldId,
            DateTimeOffset MergedAt
            );

        void RegisterDissolution(
            Guid fieldId,
            DateTimeOffset dissolvedAt
            );

        bool TryGetLineage(
            Guid fieldId,
            out GravityFieldLineageRecord? record);
            

        GravityFieldLineageState GetState();
    }
}