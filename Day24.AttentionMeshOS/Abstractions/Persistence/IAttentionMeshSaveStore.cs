//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionMeshSaveStore
    {
        AttentionMeshSaveFile? Load();

        void Save(AttentionMeshSaveFile SaveFile);
  
    }
}