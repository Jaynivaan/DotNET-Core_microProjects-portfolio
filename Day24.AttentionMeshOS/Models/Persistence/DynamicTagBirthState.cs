//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record DynamicTagBirthState(
        Guid Id,
        string Name,
        sbyte[] TernarySignature,
        int BirthMass,
        float BirthEnergy,
        float BirthStrength,
        DateTimeOffset BornAt,
        string? SemanticFingerprint,
        string? StructuralHash
        );
}