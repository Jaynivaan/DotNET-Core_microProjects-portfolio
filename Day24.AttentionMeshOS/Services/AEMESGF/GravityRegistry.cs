//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityRegistry : IGravityRegistry
    {
        private readonly Dictionary<Guid, GravityFieldRecord> _records = new();

        private readonly object _lockHandle = new();

        public int Count
        {
            get
            {
                lock(_lockHandle)
                {
                    return _records.Count;
                }
            }
        }

        public bool Register (GravityFieldRecord record)
        {
            lock (_lockHandle)
            {
                if (_records.ContainsKey(record.Id))
                {
                    return false;
                }

                _records.Add(
                    record.Id,
                    record
                    );

                return true;
            }
        }

        public bool TryGet(
            Guid id,
            out GravityFieldRecord? record)
        {
            lock( _lockHandle)
            {
                return _records.TryGetValue(id, out record);
            }
        }

        public bool Exists(Guid id)
        {
            lock( _lockHandle)
            {
                return _records.ContainsKey(id);
            }
        }

        public IReadOnlyList<GravityFieldRecord> GetAll()
        {
            lock ( _lockHandle)
            {
                var results = new List<GravityFieldRecord>(
                    _records.Count);

                foreach (var record in _records.Values )
                {
                    results.Add( record );
                }

                return results;
            }
        }
        public void Clear ()
        {
            lock (_lockHandle )
            {
                _records.Clear();
            }
        }
    }
}