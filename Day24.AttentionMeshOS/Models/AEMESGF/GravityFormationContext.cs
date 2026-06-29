//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFormationContext(
        Guid DynamicTagId,
        string DisplayName,
        sbyte[] TernarySignature,
        sbyte[] PresenceMask,
        IReadOnlyDictionary<string, int> SignalVocabulary,
        DateTimeOffset ObservedAt
        );
}