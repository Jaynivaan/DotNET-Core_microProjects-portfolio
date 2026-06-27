//gs

using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Storage
{
    public sealed class InMemoryDynamicTagBirthStore : IDynamicTagBirthStore
    {
        private readonly Dictionary<string, DynamicTagBirth> _birthRegistry = new(StringComparer.Ordinal);

        private readonly object _lockHandle = new();

        public void Register(DynamicTagBirth birth)
        {
            if (string.IsNullOrWhiteSpace(birth.Name))
            {
                return;
            }

            lock (_lockHandle)
            {
                if (_birthRegistry.ContainsKey(birth.Name))
                {
                    return;
                }

                _birthRegistry.Add(birth.Name, birth);
            }
        }

        public IReadOnlyList<DynamicTagBirth> GetAll()
        {
            lock (_lockHandle)
            {
                if (_birthRegistry.Count == 0)
                {
                    return Array.Empty<DynamicTagBirth>();
                }

                var births = new DynamicTagBirth[_birthRegistry.Count];

                int index = 0;

                foreach (var birth in _birthRegistry.Values)
                {
                    births[index++] = birth;
                }

                return births;

            }
        }

        public DynamicTagBirth? Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            lock (_lockHandle)
            {
                return _birthRegistry.TryGetValue(name, out var birth)
                    ? birth
                    : null;
            }
        }

        public bool Exists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;

            }

            lock (_lockHandle)
            {
                return _birthRegistry.ContainsKey(name);
            }
        }

        public void Clear()
        {
            _birthRegistry.Clear();
        }
    }
}