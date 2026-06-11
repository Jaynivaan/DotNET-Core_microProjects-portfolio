//gs
using Day24.AttentionMeshOS.Models;
using System;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionVelocityService
    {
        AttentionBallVelocity CalculateVelocity(
            
            Guid attentionBallId
            
            );
    }
}