//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityRegistryPersistenceSerializer
    {
        GravityRegistryState Capture();

        void Restore(            
            GravityRegistryState state);
    }
}