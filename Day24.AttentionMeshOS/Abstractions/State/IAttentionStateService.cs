//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionStateService
    {
        AttentionStateResponse GetState();
    }
}