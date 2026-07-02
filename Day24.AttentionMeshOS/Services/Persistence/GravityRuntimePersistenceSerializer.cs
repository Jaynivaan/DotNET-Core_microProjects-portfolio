//gs
using System;
using System.Linq;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityRuntimePersistenceSerializer : IGravityRuntimePersistenceSerializer
    {
        private readonly IGravityRuntime _runtime;

        public  GravityRuntimePersistenceSerializer(
            IGravityRuntime runtime)
        {
            _runtime = runtime;

        }
        
        public GravityRuntimeState Capture()
        {
            GravityFieldRuntimeState[] fields = _runtime.Fields
                .Where(field => field.IsAllocated)
                .OrderBy(field => field.FieldId)
                .Select(field => new GravityFieldRuntimeState(
                    field.FieldId,
                    field.IsAllocated,
                    field.LifecycleState,
                    field.SemanticMass,
                    field.GravityAccumulator.ToArray(),
                    field.FieldSignature.ToArray(),
                    field.Participations.Values
                        .OrderBy(participant => participant.DynamicTagId)
                        .Select(participant => new DynamicTagParticipationState(
                            participant.DynamicTagId,
                            participant.JoinedAt,
                            participant.LastReinforcedAt,
                            participant.ReinforcementCount,
                            participant.EligibleForMigration,
                            participant.PreviousFieldId))
                        .ToArray(),
                    field.CreatedAt,
                    field.LastEvolvedAt))
                .ToArray();

            return new GravityRuntimeState(fields);
        }

        public void Restore (GravityRuntimeState state)
        {
            ArgumentNullException.ThrowIfNull(state);

            _runtime.RestoreRuntimeState(
                state.Fields
                    .OrderBy(field => field.FieldId)
                    .ToArray());
        }
    }
}