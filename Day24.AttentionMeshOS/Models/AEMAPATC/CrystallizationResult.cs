//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record CrystallizationResult(
        
        bool WasProcessed ,

        bool WasCrystallized,

        string? CrystallizedTagName,

        Guid? DynamicTagBirthId,

        int? SlotIndex,

        AttentionEnergyState EnergyState,
        
        float ResonanceScore
        );
}