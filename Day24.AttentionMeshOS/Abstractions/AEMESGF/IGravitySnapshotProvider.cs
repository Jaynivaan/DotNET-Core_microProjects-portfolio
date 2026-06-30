//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravitySnapshotProvider
    {
        GravityRuntimeSnapshot GetSnapshot();
    }
}