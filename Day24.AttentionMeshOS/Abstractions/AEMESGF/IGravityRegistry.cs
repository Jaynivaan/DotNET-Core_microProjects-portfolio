//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityRegistry
    {
        int Count { get; }

        bool Register(GravityFieldRecord record);

        bool TryGet(Guid id, out GravityFieldRecord? record);

        bool Exists(Guid id);

        IReadOnlyList<GravityFieldRecord> GetAll();

        void Clear();
    }
}