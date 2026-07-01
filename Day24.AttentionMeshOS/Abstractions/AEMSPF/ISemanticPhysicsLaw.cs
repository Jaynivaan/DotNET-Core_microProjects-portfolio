//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ISemanticPhysicsLaw
    {
        SemanticPhysicsResult Evaluate(
            SemanticPhysicsContext context,
            SemanticPhysicsResult current );
    }
}