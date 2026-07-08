//gs
using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityDissolutionExecutor
    {
        bool Execute(
            GravityFieldNode field,
            DateTimeOffset dissolvedAt);
    }
}