//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMergeExecutor : IGravityMergeExecutor
    {
        private readonly IGravityRuntime _runtime;
        private readonly IGravityLineageRegistry _lineageRegistry;
        private readonly BucketMaintenanceService _bucketMaintenanceService;

        public GravityMergeExecutor(
            IGravityRuntime runtime,
            IGravityLineageRegistry lineageRegistry,
            BucketMaintenanceService bucketMaintenanceService
            )
        {
            _runtime = runtime;
            _lineageRegistry = lineageRegistry;
            _bucketMaintenanceService = bucketMaintenanceService;
        }

        public bool Execute(
            GravityFieldNode source,
            GravityFieldNode target,
            DateTimeOffset mergedAt
            )
        {
            ArgumentNullException.ThrowIfNull( source );
            ArgumentNullException.ThrowIfNull( target );

            if ( !source.IsAllocated ||
                !target.IsAllocated ||
                source.FieldId == target.FieldId)
            {
                return false;
            }

            GravityFieldNode survivor = ChooseSurvivor(source, target);
            GravityFieldNode retired = ReferenceEquals( survivor, source)
                ? target : source;

            int survivorIndex = FindRuntimeIndex(survivor);
            int retiredIndex = FindRuntimeIndex(retired);

            if (survivorIndex < 0 || retiredIndex < 0)
            {
                return false;
            }

            sbyte[] previousSurvivorSignature = survivor.FieldSignature.ToArray();
            sbyte[] retiredSignature = retired.FieldSignature.ToArray();

            CandidateFieldRef survivorRef =
                new CandidateFieldRef(
                    survivor.FieldId,
                    survivorIndex);

            CandidateFieldRef retiredRef =
                new CandidateFieldRef(
                    retired.FieldId,
                    retiredIndex);

            _bucketMaintenanceService.RemoveField(
                retiredRef,
                retiredSignature,
                retiredSignature);

            MergeParticipants(survivor, retired);
            MergeMass(survivor, retired);
            MergeAccumulator(survivor, retired);
            RebuildSignature(survivor);

            survivor.LastEvolvedAt = mergedAt;

            _lineageRegistry.RegisterMerge(
                retired.FieldId,
                survivor.FieldId,
                mergedAt);

            _bucketMaintenanceService.UpdateField(
                survivorRef,
                previousSurvivorSignature,
                previousSurvivorSignature,
                survivor.FieldSignature,
                survivor.FieldSignature);

            _runtime.ResetField(retired.FieldId);

            return true;           
        }

        private int FindRuntimeIndex (GravityFieldNode target)
        {
            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;

            for ( int i = 0; i < fields.Count; i++ )
            {
                if ( ReferenceEquals( fields[i], target ) )
                {
                    return i;
                }
            }
            return -1;
        }

        private static GravityFieldNode ChooseSurvivor(
            GravityFieldNode first,
            GravityFieldNode second)
        {
            int massCompare = second.SemanticMass.CompareTo(first.SemanticMass);
            if ( massCompare < 0 ) return first;
            if ( massCompare > 0 ) return second;

            int stabilityCompare = second.StabilityScore.CompareTo(first.StabilityScore);
            if (stabilityCompare < 0) return first;
            if (stabilityCompare > 0) return second;

            int ageCompare = first.CreatedAt.CompareTo(second.CreatedAt);
            if ( ageCompare < 0 ) return first;
            if (ageCompare > 0) return second;

            return first.FieldId.CompareTo(second.FieldId) <= 0 
                ? first 
                : second;
        }

        private static void MergeParticipants(
            GravityFieldNode survivor,
            GravityFieldNode retired)
        {
            foreach( KeyValuePair<Guid,DynamicTagParticipation> entry
                in retired.Participations)
            {
                DynamicTagParticipation incoming =
                    CopyParticipation(entry.Value);
                
                if (!survivor.Participations.TryGetValue(
                    entry.Key,
                    out DynamicTagParticipation? existing))
                {
                    survivor.Participations[entry.Key] = incoming;
                    continue;
                }

                existing.JoinedAt =
                    existing.JoinedAt <= incoming.JoinedAt
                        ? existing.JoinedAt 
                        : incoming.JoinedAt;

                existing.LastReinforcedAt = 
                    existing.LastReinforcedAt >= incoming.LastReinforcedAt
                        ? existing.LastReinforcedAt
                        : incoming.LastReinforcedAt;

                existing.ReinforcementCount +=
                    incoming.ReinforcementCount;

                existing.EligibleForMigration = 
                    existing.EligibleForMigration ||
                    incoming.EligibleForMigration;
            }
        }

        private static DynamicTagParticipation CopyParticipation(
            DynamicTagParticipation source )
        {
            return new DynamicTagParticipation
            {
                DynamicTagId = source.DynamicTagId,
                JoinedAt = source.JoinedAt,
                LastReinforcedAt = source.LastReinforcedAt,
                ReinforcementCount = source.ReinforcementCount,
                EligibleForMigration = source.EligibleForMigration,
                PreviousFieldId = source.PreviousFieldId,
            };
        }
        
        private static void MergeMass(
            GravityFieldNode survivor,
            GravityFieldNode retired)
        {
            double combined = 
                survivor.SemanticMass + retired.SemanticMass;

            survivor.SemanticMass =
                combined > float.MaxValue
                    ? float.MaxValue
                    : (float)combined;
        }

        private static void MergeAccumulator(
            GravityFieldNode survivor,
            GravityFieldNode retired)
        {
            int length = Math.Min(
                survivor.GravityAccumulator.Length,
                retired.GravityAccumulator.Length);

            for ( int i = 0; i < length; i++ )
            {
                survivor.GravityAccumulator[i] +=
                    retired.GravityAccumulator[i];
            }
        }

        private static void RebuildSignature (
            GravityFieldNode survivor)
        {
            int length = Math.Min(
                survivor.GravityAccumulator.Length,
                survivor.FieldSignature.Length);

            for ( int i = 0;i < length;i++ )
            {
                int value = survivor.GravityAccumulator[i];

                survivor.FieldSignature[i] = 
                    value > 0
                        ? (sbyte)1
                        : value < 0
                            ? (sbyte) -1 
                            : (sbyte)0;
            }
        }
    }
}