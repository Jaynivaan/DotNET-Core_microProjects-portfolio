//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ISemanticPhysicsPersistenceSerializer
    {
        SemanticPhysicsStateRecord Capture();

        void Restore(SemanticPhysicsStateRecord record);
    }
}