//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IResonanceCalculator
    {
        ResonanceResult Calculate(
            HyperVectorPayload source,
            HyperVectorPayload target);

    }

}