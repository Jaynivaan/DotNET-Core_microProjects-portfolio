//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed record RawAttentionInput(
        Guid Id,
        string Text,
        string Source,
        DateTimeOffset RecievedAt
        );
}