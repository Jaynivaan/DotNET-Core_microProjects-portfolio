//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed class DynamicTagParticipation
    {

        public Guid DynamicTagId { get; set; }
        public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastReinforcedAt { get; set; } = DateTimeOffset.UtcNow;
        public int ReinforcementCount { get; set; }
        
        //for migrations
        public bool EligibleForMigration { get; set; }
        public Guid? PreviousFieldId { get; set; }
        
    }
}