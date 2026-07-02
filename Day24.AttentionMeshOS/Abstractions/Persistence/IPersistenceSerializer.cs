//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IPersistenceSerializer
    {
        object Capture();

        void Restore(object state);
    }
}