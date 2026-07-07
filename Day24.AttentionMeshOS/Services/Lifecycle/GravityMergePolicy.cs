//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMergePolicy : IGravityMergePolicy
    {
        private readonly GravityEvolutionOptions _options;

        public GravityMergePolicy(
            IOptions<GravityEvolutionOptions> options)
        {
            _options = options.Value;
        }

        public GravityMergeDecision Decide(
            GravityMergeCandidate candidate,
            GravityFieldNode source,
            GravityFieldNode target,
            DateTimeOffset evaluationTime)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            if ( !_options.MergeEnabled)
            {
                return candidate with { DecisionReason = "Merge disabled." } is var rejected
                    ? new GravityMergeDecision(rejected, false)
                    : new GravityMergeDecision(rejected, false);
            }

            if ( source.FieldId == target.FieldId )
            {
                return new GravityMergeDecision(
                    candidate with { DecisionReason = "Same field." },
                    false);
            }

            if ( candidate.SimilarityScore < _options.MergeSimilarityThreshold)
            {
                return new GravityMergeDecision(
                    candidate with { DecisionReason = "Similarity below Threshold." },
                    false);
            }

            if ( candidate.MassRatio < _options.MergeMassThreshold)
            {
                return new GravityMergeDecision(
                    candidate with { DecisionReason = "Mass Ratio below threshold." },
                    false);
            }

            if ( evaluationTime - source.CreatedAt < _options.MinimumFieldAgeForMerge ||
                evaluationTime - target.CreatedAt < _options.MinimumFieldAgeForMerge )
            {
                return new GravityMergeDecision(
                    candidate with { DecisionReason = "Field age below merge minimum." },
                    false);
            }

            return new GravityMergeDecision(
                candidate with { DecisionReason = "Merge Approved." },
                true);
        }
    }
}