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
        private readonly ISemanticPhysicsPersistenceSerializer _physicsSerializer;

        public  GravityRuntimePersistenceSerializer(
            IGravityRuntime runtime,
            ISemanticPhysicsPersistenceSerializer physicsSerializer)
        {
            _runtime = runtime;
            _physicsSerializer = physicsSerializer;

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
                    field.LastEvolvedAt,
                    _physicsSerializer.Capture(field.Physics)))
                    
                .ToArray();

            return new GravityRuntimeState(fields);
        }

        public void Restore (GravityRuntimeState state)
        {
            ArgumentNullException.ThrowIfNull(state);

            GravityFieldRuntimeState[] fields = state.Fields
                .OrderBy(field => field.FieldId)
                .ToArray();

            _runtime.RestoreRuntimeState(fields);

            foreach( GravityFieldRuntimeState savedField in fields )
            {
                GravityFieldNode? runtimeField = _runtime.Fields
                    .FirstOrDefault(field => 
                        field.IsAllocated &&
                        field.FieldId == savedField.FieldId);

                if (runtimeField is null )
                {
                    continue;
                }

                _physicsSerializer.Restore(
                    runtimeField.Physics,
                    savedField.Physics);

                runtimeField.AttentionEnergy = runtimeField.Physics.AttentionEnergy;
                runtimeField.StabilityScore = runtimeField.Physics.Stability;
                runtimeField.FieldRadius = runtimeField.Physics.Radius;
            }
        }
    }
}