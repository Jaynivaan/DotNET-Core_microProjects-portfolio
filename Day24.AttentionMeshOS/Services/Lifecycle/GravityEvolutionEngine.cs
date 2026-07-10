//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityEvolutionEngine : IGravityEvolutionEngine
    {
        private readonly ILogger<GravityEvolutionEngine> _logger;
        private readonly IGravityMergeEvaluator _mergeEvaluator;
        private readonly IGravityMergePolicy _mergePolicy;
        private readonly IGravityMergeExecutor _mergeExecutor;
        private readonly IGravityDissolutionPolicy _dissolutionPolicy;
        private readonly IGravityDissolutionExecutor _dissolutionExecutor;
        private readonly GravityEvolutionOptions _options;

        public GravityEvolutionEngine(
            ILogger<GravityEvolutionEngine> logger,
            IGravityMergeEvaluator mergeEvaluator,
            IGravityMergePolicy mergePolicy,
            IGravityMergeExecutor mergeExecutor,
            IGravityDissolutionPolicy dissolutionPolicy,
            IGravityDissolutionExecutor dissolutionExecutor,
            IOptions<GravityEvolutionOptions> options
            )
        {
            _logger = logger;
            _mergeEvaluator = mergeEvaluator;
            _mergePolicy = mergePolicy;
            _mergeExecutor = mergeExecutor;
            _dissolutionPolicy = dissolutionPolicy;
            _dissolutionExecutor = dissolutionExecutor;
            _options = options.Value;
        }

        public GravityEvolutionResult Evaluate (
            GravityEvolutionContext context )
        {
            ArgumentNullException.ThrowIfNull(context);

            IReadOnlyList<GravityFieldNode> fields = context.Fields;

            AemEsgfTelemetry.GravityEvolutionStarted(
                _logger,
                fields.Count,
                context.EvaluationTime
                );

            int mergeCandidatesEvaluated = 0;
            int mergesExecuted = 0;
            int dissolutionCandidatesEvaluated = 0;
            int dissolutionsExecuted = 0;

            

            //pass  1 convergeence and merging..
            for (int i = 0; i < fields.Count; i++)
            {
                GravityFieldNode source = fields[i];

                if (!source.IsAllocated)
                {
                    continue;
                }

                for ( int j = 0; j < fields.Count; j++)
                {
                    GravityFieldNode target = fields[j];

                    if (!target.IsAllocated)
                    {
                        continue;
                    }

                    if (mergeCandidatesEvaluated >= _options.MaximumMergeCandidates)
                    {
                        break;
                    }

                    GravityMergeCandidate candidate =
                        _mergeEvaluator.Evaluate(source, target);

                    AemEsgfTelemetry.GravityMergeCandidateEvaluated(
                        _logger,
                        candidate.SourceFieldId,
                        candidate.TargetFieldId,
                        candidate.SimilarityScore,
                        candidate.MassRatio,
                        candidate.StabilityScore);

                    mergeCandidatesEvaluated++;

                    GravityMergeDecision decision =
                        _mergePolicy.Decide(
                            candidate,
                            source,
                            target,
                            context.EvaluationTime);

                    AemEsgfTelemetry.GravityMergeDecisionCompleted(
                        _logger,
                        candidate.SourceFieldId,
                        candidate.TargetFieldId,
                        decision.Approved,
                        decision.Candidate.DecisionReason
                        );

                    if (!decision.Approved)
                    {
                        continue;
                    }

                    if ( _mergeExecutor.Execute(
                        source,
                        target,
                        context.EvaluationTime))
                    {
                        mergesExecuted++;

                        AemEsgfTelemetry.GravityMergeExecuted(
                            _logger,
                            source.FieldId,
                            target.FieldId,
                            context.EvaluationTime
                            );

                        break;
                    }
                }
            }
            for ( int i = 0; i < fields.Count; ++i)
            {
                GravityFieldNode field = fields[i];

                if (!field.IsAllocated)
                {
                    continue;
                }

                GravityDissolutionCandidate candidate =
                    BuildDissolutionCandidate(
                        field,
                        context.EvaluationTime);

                dissolutionCandidatesEvaluated++;

                AemEsgfTelemetry.GravityDissolutionCandidateEvaluated(
                    _logger,
                    candidate.FieldId,
                    candidate.AttentionEnergy,
                    candidate.Stability,
                    candidate.SemanticMass,
                    candidate.ParticipantCount);

                

                GravityDissolutionDecision decision =
                    _dissolutionPolicy.Evaluate(candidate);

                AemEsgfTelemetry.GravityDissolutionDecisionCompleted(
                    _logger,
                    candidate.FieldId,
                    decision.Approved,
                    decision.Reason
                    );
                
                if (!decision.Approved)
                {
                    continue;
                }

                if (_dissolutionExecutor.Execute(
                   field,
                   context.EvaluationTime))
                {
                    dissolutionsExecuted++;

                    AemEsgfTelemetry.GravityFieldDissolved(
                        _logger,
                        field.FieldId,
                        context.EvaluationTime
                        );
                }
            }

            AemEsgfTelemetry.GravityEvolutionCompleted(
                _logger,
                mergeCandidatesEvaluated,
                mergesExecuted,
                dissolutionCandidatesEvaluated,
                dissolutionsExecuted,
                mergesExecuted > 0 || dissolutionsExecuted > 0);

            return new GravityEvolutionResult(
                mergeCandidatesEvaluated,
                mergesExecuted,
                dissolutionCandidatesEvaluated,
                dissolutionsExecuted,
                mergesExecuted > 0 || dissolutionsExecuted > 0);

            
        }
        private static GravityDissolutionCandidate BuildDissolutionCandidate(
            GravityFieldNode field,
            DateTimeOffset evaluationTime)
        {
            return new GravityDissolutionCandidate(

            FieldId: field.FieldId,
            AttentionEnergy: field.AttentionEnergy,
            Stability: field.StabilityScore,
            SemanticMass: field.SemanticMass,
            ParticipantCount: field.Participations.Count,
            FieldAge: evaluationTime - field.CreatedAt,
            CreatedAt: field.CreatedAt,
            LastEvolvedAt: field.LastEvolvedAt,
            IsDominantField: false,
            RecentlyReinforced: false,
            Reason: "Dissolution candidate Evaluated." );
        }            
    }
}