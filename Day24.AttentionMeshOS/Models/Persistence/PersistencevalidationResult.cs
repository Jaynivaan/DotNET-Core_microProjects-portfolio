//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record PersistenceValidationResult(
        bool Succeeded,
        string Message
        );
}