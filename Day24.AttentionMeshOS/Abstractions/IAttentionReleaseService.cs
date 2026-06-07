//gs
using System;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionReleaseService
    {
        bool Release(Guid attentionBallId);
    }
}