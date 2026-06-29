//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMembershipManager
    {
        public bool AddParticipant(
            GravityFieldNode field,
            Guid dynamicTagId,
            GravityOptions options)
        {
            if (!field.IsAllocated)
            {
                return false;
            }

            if (field.ParticipatingDynamicTagIds.Contains(dynamicTagId))
            {
                field.LastEvolvedAt = DateTimeOffset.UtcNow;
                return true;
            }

            if (field.ParticipatingDynamicTagIds.Count >=
                options.MaxDynamicTagsPerField)
            {
                return false;
            }

            field.ParticipatingDynamicTagIds.Add(dynamicTagId);
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
                field.ParticipatingDynamicTagIds.Remove(dynamicTagId);

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

            return field.ParticipatingDynamicTagIds.Contains( dynamicTagId);
        }
    }


}
