//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMembershipManager
    {
        private readonly ILogger<GravityMembershipManager> _logger;

        public GravityMembershipManager(
            ILogger<GravityMembershipManager> logger)
        {
            _logger = logger;
        }

        public bool AddParticipant(
            GravityFieldNode field,
            Guid dynamicTagId,
            GravityOptions options)
        {
            if (!field.IsAllocated)
            {
                return false;
            }

            if (field.Participations.TryGetValue(
                dynamicTagId,
                out var participation))
            {
                participation.LastReinforcedAt = DateTimeOffset.UtcNow;
                participation.ReinforcementCount++;

                AemEsgfTelemetry.ParticipationReinforced(
                _logger,
                field.FieldId,
                dynamicTagId,
                participation.ReinforcementCount);

                field.LastEvolvedAt = DateTimeOffset.UtcNow;
                return true;
            }

            if (field.Participations.Count >=
                options.MaxDynamicTagsPerField)
            {
                return false;
            }

            field.Participations.Add(
                dynamicTagId,
                new DynamicTagParticipation
                {
                    DynamicTagId = dynamicTagId,
                    JoinedAt = DateTimeOffset.UtcNow,
                    LastReinforcedAt = DateTimeOffset.UtcNow,
                    ReinforcementCount =1
                });

            field.LastEvolvedAt = DateTimeOffset.UtcNow;

            return true;
        }
        private bool RemoveParticipant(
            GravityFieldNode field,
            Guid dynamicTagId)
        {
            if (!field.IsAllocated)
            {
                return false;
            }

            bool removed = 
                field.Participations.Remove(dynamicTagId);

            if (removed)
            {
                field.LastEvolvedAt = DateTimeOffset.UtcNow;
            }

            return removed;
        }

        public bool ContainsParticipant(
            GravityFieldNode field,
            Guid dynamicTagId)
        {
            if ( !field.IsAllocated )
            {
                return false;
            }

            return field.Participations.ContainsKey( dynamicTagId);
        }
    }


}
