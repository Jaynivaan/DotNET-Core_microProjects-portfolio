//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record DeleteResponse(
        bool Succeeded,
        string Message,
        int DeletedCount = 0
        );
}