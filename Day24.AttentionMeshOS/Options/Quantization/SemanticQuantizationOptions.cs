//gs
using System.ComponentModel.DataAnnotations;


namespace Day24.AttentionMeshOS.Options
{
    public sealed class SemanticQuantizationOptions
    {
        public bool Enabled { get; init; } = true;

        [Range(0, 8)]
        public int NeighborRadius { get; init; } = 1;

        [Range(0, int.MaxValue)]
        public int MaximumBucketSize { get; init; } = 9999;
        
    }
}