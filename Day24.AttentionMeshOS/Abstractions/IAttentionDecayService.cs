//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionDecayService
    {
        //interfaces for services that decay attentionBall
        //
        AttentionBall ApplyDecay(AttentionBall attentionBall);

        AttentionBall Boost(AttentionBall attentionBall);
    }
}