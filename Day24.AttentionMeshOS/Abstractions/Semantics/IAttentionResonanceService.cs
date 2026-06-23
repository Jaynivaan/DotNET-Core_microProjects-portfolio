//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionResonanceService
    {
        double CalculateResonance(
            AttentionBall source,
            AttentionBall target);
    }
}
//this is a semantic orchestrasion bridge towards the meshbuilder...
//where as IResonancecalculator is abstraction for the math functionality..


