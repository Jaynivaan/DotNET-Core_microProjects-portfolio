//gs
using System.ComponentModel.DataAnnotations;

namespace Day24.AttentionMeshOS.Options
{
    public sealed class GravityEvolutionOptions
    {
        public bool MergeEnabled { get; init; } = true;

        public bool DissolutionEnabled { get; init; } = true;
        
        public bool MigrationEnabled { get; init; } = false;

        [Range(0.0, 1.0)]
        public double MergeSimilarityThreshold { get; init; } = 0.90;

        [Range(0.0, double.MaxValue)]
        public double MergeMassThreshold { get; init; } = 1.0;

        [Range(0.0, double.MaxValue)]
        public double DissolutionEnergyThreshold { get; init; } = 0.10;

        [Range(0.0, double.MaxValue)]
        public double DissolutionStabilityThreshold { get; init; } = 0.10;

        public TimeSpan MinimumFieldAgeForMerge { get; init; }
            = TimeSpan.FromMinutes(5);

        public TimeSpan MinimumFieldAgeForDissolution { get; init; }
            = TimeSpan.FromMinutes(10);

        [Range(1, int.MaxValue)]
        public int MaximumMergeCandidates { get; init; } = 10;

        [Range(0.0, 1.0)]
        public double MigrationSimilarityThreshold { get; init; } = 0.75;
    }
}