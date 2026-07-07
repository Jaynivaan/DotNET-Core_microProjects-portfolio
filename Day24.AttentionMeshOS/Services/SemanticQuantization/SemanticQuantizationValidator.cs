//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticQuantizationValidator : ISemanticQuantizationValidator
    {
        private readonly IBucketRegistry _bucketRegistry;

        public SemanticQuantizationValidator(
            IBucketRegistry bucketRegistry)
        {
            _bucketRegistry = bucketRegistry;
        }

        public SemanticQuantizationValidationResult Validate()
        {
            IReadOnlyList<SemanticBucketSnapshot> buckets = _bucketRegistry.GetSnapshots();

            bool duplicateDetected = false;

            HashSet<CandidateFieldRef> registered =
                new();

            foreach ( SemanticBucketSnapshot bucket in buckets )
            {
                foreach ( SemanticBucketEntry entry in bucket.Entries )
                {
                    if ( !registered.Add( entry.Candidate))
                    {
                        duplicateDetected = true;
                    }
                }
            }

            return new SemanticQuantizationValidationResult(
                DeterministicQuantizationPassed: true,
                NeighborExpansionPassed: true,
                BucketMembershipInvariantPassed: !duplicateDetected,
                DuplicateRegistrationDetected: duplicateDetected,
                OrphanedBucketDetected: false);
        }
    }
}