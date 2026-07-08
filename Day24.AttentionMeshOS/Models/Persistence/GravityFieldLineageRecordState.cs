//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFieldLineageRecordState(
        Guid FieldId,
        IReadOnlyList<Guid> OriginFieldIds,
        IReadOnlyList<Guid> ParentFieldIds,
        Guid? MergedIntoFieldId,
        DateTimeOffset CreatedAt,
        DateTimeOffset? MergedAt,
        DateTimeOffset? DissolvedAt,
        string LineageReason);
}