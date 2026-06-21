//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed class InputProcessingContext
    {
        public RawAttentionInput RawInput { get; }

        public InputValidationResult ValidationResult { get; } = new();

        public bool IsApprovedForEngine => ValidationResult.IsValid;

        public NormalizedInput? NormalizedInput { get; set; }

        public NoiseReducedInput? NoiseReducedInput { get; set; }

        public KeywordExtractionResult? KeywordExtractionResult { get; set; }

        public TagExtractionResult? TagExtractionResult { get; set; }

        public VectorPreparationResult? VectorPreparationResult { get; set; }

        public string EffectiveText =>
            NoiseReducedInput?.ReducedText
            ?? NormalizedInput?.NormalizedText
            ?? RawInput.Text;

        public InputProcessingContext (RawAttentionInput rawInput)
        {
            RawInput = rawInput;
        }

        
    }
}