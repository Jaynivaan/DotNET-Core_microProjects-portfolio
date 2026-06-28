//gs
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IDynamicTagRegistry
    {
        void Register(DynamicTagBirth birth);

        IReadOnlyList<DynamicTagBirth> GetAll();

        DynamicTagBirth? Get(string name);

        int GetCount();

        bool Exists(string name);

        void Clear();

        
    }
}