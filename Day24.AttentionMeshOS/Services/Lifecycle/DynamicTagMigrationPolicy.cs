//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class DynamicTagMigrationPolicy : IDynamicTagMigrationPolicy
    {
        private readonly GravityEvolutionOptions _options;

        public DynamicTagMigrationPolicy(
            IOptions<GravityEvolutionOptions> options)
        {
            _options = options.Value;
        }

        public DynamicTagMigrationDecision Evaluate (
            DynamicTagMigrationCandidate candidate)
        {
            if ( !_options.MigrationEnabled )
            {
                return new DynamicTagMigrationDecision(false, "Migration disabled.");
            }

            if ( !candidate.SourceRetiring )
            {
                return new DynamicTagMigrationDecision(false, "Source field not retiring.");
            }

            if ( candidate.SimilarityScore < _options.MigrationSimilarityThreshold )
            {
                return new DynamicTagMigrationDecision(false, "Similarity below threshold.");
            }

            if ( candidate.TargetStability < candidate.SourceStability )
            {
                return new DynamicTagMigrationDecision(false, "Target field less stable.");
            }

            if ( candidate.SourceFieldId == candidate.TargetFieldId )
            {
                return new DynamicTagMigrationDecision(false, "Source and Taget identical or same.");
            }

            return new DynamicTagMigrationDecision(true, "Approved for migration.");
        }
    }

}