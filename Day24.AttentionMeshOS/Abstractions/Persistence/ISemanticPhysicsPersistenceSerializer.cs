//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ISemanticPhysicsPersistenceSerializer
    {
        SemanticPhysicsStateRecord Capture(SemanticPhysicsState state);

        void Restore(
            SemanticPhysicsState state,
            SemanticPhysicsStateRecord record);
    }
}