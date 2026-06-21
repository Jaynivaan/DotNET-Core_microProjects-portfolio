//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record VectorPreparationResult(
        string TextForEmbedding,
        IReadOnlyList<string> Keywords,
        IReadOnlyList<string> Tags,
        DateTimeOffset PreparedAt
        );
}