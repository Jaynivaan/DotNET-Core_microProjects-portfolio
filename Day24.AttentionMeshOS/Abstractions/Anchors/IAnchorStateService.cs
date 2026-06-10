//gs
using Day24.AttentionMeshOS.Models;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAnchorStateService
    {
        IReadOnlyList<AnchorAttentionResponse> GetAnchors();
    }
}