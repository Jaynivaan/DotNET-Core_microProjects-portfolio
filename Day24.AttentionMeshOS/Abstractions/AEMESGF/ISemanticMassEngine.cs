//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ISemanticMassEngine
    {
        SemanticMassResult UpdateMass(
            GravityFieldNode field,
            GravityFormationContext context,
            float resonanceScore);
    }
}