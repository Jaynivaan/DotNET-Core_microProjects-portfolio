//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed record InvalidInputResponse(
        Guid RawInputId,

        string Message,

        IReadOnlyList<string> Errors
        );
}