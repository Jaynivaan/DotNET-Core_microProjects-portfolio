//gs
using System;

using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionReleaseService
    {
        DeleteResponse Release(Guid attentionBallId);

        DeleteResponse ReleaseAll();
    }
}