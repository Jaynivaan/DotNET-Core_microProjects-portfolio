//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed class CandidateResolutionMetrics
    {
        public long TotalResolutions { get; set; }

        public long TotalCandidatesReturned { get; set; }

        public long FallbackCount { get; set; }

        public double AverageCandidateCount { get; set; }

        public string LastResolverName { get; set; } = string.Empty;

        public int LastCandidateCount { get; set; }
    }
}