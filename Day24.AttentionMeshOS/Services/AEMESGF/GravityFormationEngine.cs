//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFormationEngine : IGravityFormationEngine
    {
        private readonly GravityFieldSelectionEngine _selectionEngine;
        private readonly GravityMembershipManager _membershipManager;
        private readonly GravityFieldSignatureUpdater _signatureUpdater;
        private readonly ISemanticMassEngine _semanticMassEngine;
        private readonly GravityFieldFactory _factory;
        private readonly IGravityLifecycleManager _lifecycleManager;
        private readonly GravityOptions _options;


        public GravityFormationEngine(
            GravityFieldSelectionEngine selectionEngine,
            GravityMembershipManager membershipManager,
            GravityFieldSignatureUpdater signatureUpdater,
            ISemanticMassEngine semanticMassEngine,
            GravityFieldFactory factory,
            IGravityLifecycleManager lifecycleManager,
            IOptions<GravityOptions> options )
        {
            _selectionEngine = selectionEngine;
            _membershipManager = membershipManager;
            _signatureUpdater = signatureUpdater;
            _semanticMassEngine = semanticMassEngine;
            _factory = factory;
            _lifecycleManager = lifecycleManager;
            _options = options.Value;
        }

        public GravityFormationResult Process ( GravityFormationContext context )
        {
            GravityFieldSelectionResult selection =
               _selectionEngine.SelectField(context);

            if ( selection.MatchFound &&
                selection.Field is not null)
            {
                GravityFieldNode field = selection.Field;

                _membershipManager.AddParticipant(
                    selection.Field,
                    context.DynamicTagId,
                    _options);

                _signatureUpdater.Update(
                    selection.Field,
                    context.TernarySignature,
                    _options);

                SemanticMassResult matchedMassResult =
                    _semanticMassEngine.UpdateMass(
                    selection.Field,
                    context,
                    selection.ProximityScore);

                GravityLifecycleEvaluationResult lifecycleResult =
                    _lifecycleManager.Evaluate(field);



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

            SemanticMassResult createdMassResult =
                    _semanticMassEngine.UpdateMass(
                    newField,
                    context,
                    1.0f);

            GravityLifecycleEvaluationResult createdLifecycleResult =
                _lifecycleManager.Evaluate(newField);
                            

            return new GravityFormationResult(
                WasProcessed: true,
                FieldCreated: true,
                FieldMatched: false,
                GravityFieldId: newField.FieldId,
                ProximityScore: 1.0f,
                LifecycleState: createdLifecycleResult.CurrentState);
        }
    }
}