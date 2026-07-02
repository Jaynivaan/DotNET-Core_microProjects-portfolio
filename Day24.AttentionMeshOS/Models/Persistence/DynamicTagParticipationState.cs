//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record DynamicTagParticipationState(
        Guid DynamicTagId,
        DateTimeOffset JoinedAt,
        DateTimeOffset LastReinforcedAt,
        int ReinforcementCount,
        bool EligibleForMigration,
        Guid? PreviousFieldId

        );
}