//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFormationEngine : IGravityFormationEngine
    {
        private readonly IGravityRuntime _runtime;
        private readonly BucketMaintenanceService _bucketMaintenanceService;
        private readonly GravityFieldSelectionEngine _selectionEngine;
        private readonly GravityMembershipManager _membershipManager;
        private readonly GravityFieldSignatureUpdater _signatureUpdater;
        private readonly ISemanticMassEngine _semanticMassEngine;
        private readonly GravityFieldFactory _factory;
        private readonly IGravityLifecycleManager _lifecycleManager;
        private readonly ISemanticPhysicsFramework _physicsFramework;
        private readonly ParticipationMetricsProvider _participationMetricsProvider;
        private readonly GravityOptions _options;
        private readonly SemanticPhysicsOptions _physicsOptions;
        private readonly ILogger<GravityFormationEngine> _logger;

        public GravityFormationEngine(
            IGravityRuntime runtime,
            BucketMaintenanceService bucketMaintenanceService,
            GravityFieldSelectionEngine selectionEngine,
            GravityMembershipManager membershipManager,
            GravityFieldSignatureUpdater signatureUpdater,
            ISemanticMassEngine semanticMassEngine,
            GravityFieldFactory factory,
            IGravityLifecycleManager lifecycleManager,
            ISemanticPhysicsFramework physicsFramework,
            ParticipationMetricsProvider participationMetricsProvider,
            IOptions<GravityOptions> options,
            IOptions<SemanticPhysicsOptions> physicsOptions,
            ILogger<GravityFormationEngine>logger)
        {
            _runtime = runtime;
            _bucketMaintenanceService = bucketMaintenanceService;
            _selectionEngine = selectionEngine;
            _membershipManager = membershipManager;
            _signatureUpdater = signatureUpdater;
            _semanticMassEngine = semanticMassEngine;
            _factory = factory;
            _lifecycleManager = lifecycleManager;
            _physicsFramework = physicsFramework;
            _participationMetricsProvider = participationMetricsProvider;
            _options = options.Value;
            _physicsOptions = physicsOptions.Value;
            _logger = logger;
        }

        public GravityFormationResult Process ( GravityFormationContext context )
        {
            _logger.LogInformation(
                "GFE 1: before selection.");

            GravityFieldSelectionResult selection =
               _selectionEngine.SelectField(context);

            _logger.LogInformation(
                "GFE 2: after selection. MatchFound={MatchFound}",
                selection.MatchFound);

            if ( selection.MatchFound &&
                selection.Field is not null)
            {
                GravityFieldNode field = selection.Field;

                sbyte[] previousSignature = field.FieldSignature.ToArray();


                _membershipManager.AddParticipant(
                    field,
                    context.DynamicTagId,
                    _options);

                _signatureUpdater.Update(
                    field,
                    context.TernarySignature,
                    _options);

                UpdateBucketMembership(
                    field,
                    previousSignature);

                SemanticMassResult matchedMassResult =
                    _semanticMassEngine.UpdateMass(
                    field,
                    context,
                    selection.ProximityScore);

                GravityLifecycleEvaluationResult lifecycleResult =
                    _lifecycleManager.Evaluate(field);

                EvaluateAndCommitPhysics(
                    field,
                    selection.ProximityScore,
                    lifecycleResult.CurrentState);

                return new GravityFormationResult(
                    WasProcessed: true,
                    FieldCreated: false,
                    FieldMatched: true,
                    GravityFieldId: field.FieldId,
                    ProximityScore: selection.ProximityScore,
                    LifecycleState: lifecycleResult.CurrentState);
            }

            GravityFieldNode? newField = _factory.Create(context);

            if ( newField is null )
            {
                return new GravityFormationResult(
                    WasProcessed: false,
                    FieldCreated: false,
                    FieldMatched: false,
                    GravityFieldId: null,
                    ProximityScore: 0f,
                    LifecycleState: GravityFieldLifecycleState.Dormant);
            }

            RegisterBucketMembership(newField);


            SemanticMassResult createdMassResult =
                    _semanticMassEngine.UpdateMass(
                        newField,
                        context,
                        1.0f);

            GravityLifecycleEvaluationResult createdLifecycleResult =
                _lifecycleManager.Evaluate(newField);

            EvaluateAndCommitPhysics(
                newField,
                1.0f,
                createdLifecycleResult.CurrentState);
                            

            return new GravityFormationResult(
                WasProcessed: true,
                FieldCreated: true,
                FieldMatched: false,
                GravityFieldId: newField.FieldId,
                ProximityScore: 1.0f,
                LifecycleState: createdLifecycleResult.CurrentState);
        }

        private void RegisterBucketMembership(
            GravityFieldNode field)
        {
            int runtimeIndex = FindRuntimeIndex(field);

            if ( runtimeIndex <  0 )
            {
                return;
            }

            CandidateFieldRef candidate =
                new CandidateFieldRef(
                    field.FieldId,
                    runtimeIndex);

            _bucketMaintenanceService.RegisterField(
                candidate,
                field.FieldSignature,
                field.FieldSignature);
                
        }

        private void UpdateBucketMembership(
            GravityFieldNode field,
            sbyte[] previousSignature)
        {
            int runtimeIndex = FindRuntimeIndex(field);

            if (runtimeIndex < 0 )
            {
                return;
            }

            CandidateFieldRef candidate = (
                new CandidateFieldRef(
                    field.FieldId,
                    runtimeIndex));

            _bucketMaintenanceService.UpdateField(
                candidate,
                previousSignature,
                previousSignature,
                field.FieldSignature,
                field.FieldSignature);

        }

        private int FindRuntimeIndex (
            GravityFieldNode target)
        {
            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;

            for ( int i = 0; i < fields.Count; i++ )
            {
                if (ReferenceEquals(fields[i], target))
                {
                    return i;
                }
            }
            return -1;
        }

        private void EvaluateAndCommitPhysics(
            GravityFieldNode field,
            float resonanceScore,
            GravityFieldLifecycleState lifecycleState)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            ParticipationMetrics participationMetrics =
                _participationMetricsProvider.GetMetrics(field);

            SemanticPhysicsContext physicsContext =
                new SemanticPhysicsContext(
                    field,
                    field.Physics,
                    participationMetrics,
                    field.SemanticMass,
                    resonanceScore,
                    lifecycleState,
                    _physicsOptions,
                    now);

            SemanticPhysicsResult physicsResult = 
                _physicsFramework.Evaluate(physicsContext);

            field.Physics.CapturePrevious(now);

            AemEsgfTelemetry.PreviousPhysicsStateCaptured(
                _logger,
                field.FieldId);


            field.Physics.AttentionEnergy = physicsResult.AttentionEnergy;
            field.Physics.Stability = physicsResult.Stability;
            field.Physics.Radius = physicsResult.Radius;
            field.Physics.AttractionPotential = physicsResult.AttractionPotential;
            field.Physics.SemanticMomentum = physicsResult.SemanticMomentum;

            field.AttentionEnergy = physicsResult.AttentionEnergy;
            field.StabilityScore = physicsResult.Stability;
            field.FieldRadius = physicsResult.Radius;

            field.LastEvolvedAt = now;

            AemEsgfTelemetry.PhysicsStateCommitted(
                _logger,
                field.FieldId,
                physicsResult.AttentionEnergy,
                physicsResult.Stability,
                physicsResult.Radius,
                physicsResult.AttractionPotential,
                physicsResult.SemanticMomentum);
        }
    }
}