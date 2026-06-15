//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class AttentionInputValidationOptions
    {
        public bool EnableValidation { get; set; } = true;

        public int MinimumTextLength { get; set; } = 2;

        public int MaximumTextLength { get; set; } = 4000;

        public bool RejectWhitespacesOnly { get; set; } = true;

    }
}