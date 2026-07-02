//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record DynamicTagRegistryState(
        IReadOnlyList<DynamicTagBirthState>Tags
        );
}