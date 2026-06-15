//gs
using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IRawAttentionInputStore
    {
        void Save(RawAttentionInput rawInput);

        void Update(RawAttentionInput rawInput);

        IReadOnlyList<RawAttentionInput> GetAll();
    }
}