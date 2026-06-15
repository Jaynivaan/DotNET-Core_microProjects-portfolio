//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed class InputValidationResult
    {
        public bool IsValid => Errors.Count == 0;

        public List<string> Errors { get; } = new();

    }
}