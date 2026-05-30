//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class AttentionOptions
    {
        public double DecayRate { get; set; } = 0.01;

        public double MinimumAttentionWeight { get; set; } = 0.10;

        public double AttentionBoost { get; set; } = 0.05;

        public double AnchorDecayMultiplier { get; set; } = 0.25;

        public double AnchorMinimumWeight { get; set; } = 0.5;


    }
}