//gs
using System;
using System.Linq;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class DynamicTagPersistenceSerializer : IDynamicTagPersistenceSerializer
    {
        private readonly IDynamicTagRegistry _registry;

        public DynamicTagPersistenceSerializer(
            IDynamicTagRegistry registry)
        {
            _registry = registry;
        }

        public DynamicTagRegistryState Capture()
        {
            DynamicTagBirthState[] tags = _registry
                .GetAll()
                .OrderBy(tags => tags.Name, StringComparer.Ordinal)
                .Select(tag => new DynamicTagBirthState(
                    tag.Id,
                    tag.Name,
                    tag.TernarySignature.ToArray(),
                    tag.BirthMass,
                    tag.BirthEnergy,
                    tag.BirthStrength,
                    tag.BornAt,
                    SemanticFingerprint: null,
                    StructuralHash: null))
                .ToArray();
            return new DynamicTagRegistryState(tags);
        }

        public void Restore (DynamicTagRegistryState state)
        {
            ArgumentNullException.ThrowIfNull(state);
            
            _registry.Clear ();

            foreach (DynamicTagBirthState tag in state.Tags)
            {
                if (_registry.Exists(tag.Name))
                {
                    continue;
                }

                _registry.Register(
                    new DynamicTagBirth(
                        tag.Id,
                        tag.Name,
                        tag.TernarySignature.ToArray(),
                        tag.BirthMass,
                        tag.BirthEnergy,
                        tag.BirthStrength,
                        tag.BornAt));
            }
        }
    }
}