//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed class InputProcessingContext
    {
        public RawAttentionInput RawInput { get; }

        public InputValidationResult ValidationResult { get; } = new();

        public bool IsApprovedForEngine => ValidationResult.IsValid;

        public NormalizedInput? NormalizedInput { get; set; }

        public InputProcessingContext (RawAttentionInput rawInput)
        {
            RawInput = rawInput;
        }

        
    }
}