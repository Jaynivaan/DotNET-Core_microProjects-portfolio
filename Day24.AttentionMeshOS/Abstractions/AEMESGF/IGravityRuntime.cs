//gs
using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityRuntime
    {
        IReadOnlyList<GravityFieldNode> Fields { get; }        

        int FieldCount { get; }

        int AllocatedFieldCount { get; }

        TimeSpan Uptime { get; }

        bool TryAllocateField(out GravityFieldNode? field);

        bool ResetField(Guid fieldId);
    }
}