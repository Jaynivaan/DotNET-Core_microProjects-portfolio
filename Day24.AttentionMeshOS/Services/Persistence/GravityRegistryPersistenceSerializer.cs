//gs
using System;
using System.Linq;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityRegistryPersistenceSerializer : IGravityRegistryPersistenceSerializer
    {
        private readonly IGravityRegistry  _registry;

        public GravityRegistryPersistenceSerializer(
            IGravityRegistry registry)
        {
            _registry = registry;
        }

        public GravityRegistryState Capture()
        {
            GravityFieldIdentityState[] fields = _registry
                .GetAll()
                .OrderBy(record => record.Id)
                .Select(record => new GravityFieldIdentityState(
                    record.Id,
                    record.DisplayName,
                    record.CreatedAt,
                    SemanticFingerprint: null,
                    StructuralHash: null,
                    OriginEventId: null,
                    ParentIds: Array.Empty<Guid>()))
                .ToArray();
            return new GravityRegistryState(fields);
        }

        public void Restore(
            GravityRegistryState state)
        {
            ArgumentNullException.ThrowIfNull(state);

            _registry.Clear();

            foreach (GravityFieldIdentityState field in state.Fields)
            {
                _registry.Register(
                    new GravityFieldRecord(
                        field.FieldId,
                        field.DisplayName,
                        field.CreatedAt));
            }
        }
    }
}