//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class ResonanceOptions
    {
        public bool Enabled { get; set; } = true;

        public double TextWeight { get; set; } = 0.35;

        public double HyperVectorWeight { get; set; } = 0.65;

        public double MinimumScore { get; set; } = 0.10;
    }
}