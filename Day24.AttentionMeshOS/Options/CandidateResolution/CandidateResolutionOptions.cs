//gs

using System.ComponentModel.DataAnnotations;


namespace Day24.AttentionMeshOS.Options
{
    public sealed class CandidateResolutionOptions
    {
        public bool Enabled { get; set; }

        [Required]
        public string ResolverType { get; set; } = "AllFields";

        [Range(1, int.MaxValue)]
        public int FingerprintBlockSize { get; set; } = 8;

        [Range(0, int.MaxValue)]
        public int MinimumCandidateCount { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int MaximumCandidateCount { get; set; } = 256;

        public bool AllowFallbackToAllFields { get; set; } = true;

    }
}