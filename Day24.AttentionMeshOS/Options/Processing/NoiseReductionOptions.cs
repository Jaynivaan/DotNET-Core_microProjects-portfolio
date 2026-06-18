//gs

namespace Day24.AttentionMeshOS.Options
{
    public sealed class NoiseReductionOptions
    {
        public bool Enabled { get; set; } = false;

        public string Level { get; set; } = "Light"; // other levels are "Medium" and "Aggressive"

    }
}