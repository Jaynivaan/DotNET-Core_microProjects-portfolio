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
        private readonly GravityFieldFactory _factory;
        private readonly GravityOptions _options;


        public GravityFormationEngine(
            GravityFieldSelectionEngine selectionEngine,
            GravityMembershipManager membershipManager,
            GravityFieldSignatureUpdater signatureUpdater,
            GravityFieldFactory factory,
            IOptions<GravityOptions> options )
        {
            _selectionEngine = selectionEngine;
            _membershipManager = membershipManager;
            _signatureUpdater = signatureUpdater;
            _factory = factory;
            _options = options.Value;
        }

        public GravityFormationResult Process ( GravityFormationContext context )
        {
            GravityFieldSelectionResult selection =
               _selectionEngine.SelectField(context);

            if ( selection.MatchFound &&
                selection.Field is not null)
            {
                _membershipManager.AddParticipant(
                    selection.Field,
                    context.DynamicTagId,
                    _options);

                _signatureUpdater.Update(
                    selection.Field,
                    context.TernarySignature,
                    _options);

                return new GravityFormationResult(
                    WasProcessed: true,
                    FieldCreated: false,
                    FieldMatched: true,
                    GravityFieldId: selection.Field.FieldId,
                    ProximityScore: selection.ProximityScore,
                    LifecycleState: selection.Field.LifecycleState);
            }

            GravityFieldNode? field = _factory.Create(context);

            if ( field is null )
            {
                return new GravityFormationResult(
                    WasProcessed: false,
                    FieldCreated: false,
                    FieldMatched: false,
                    GravityFieldId: null,
                    ProximityScore: 0f,
                    LifecycleState: GravityFieldLifecycleState.Dormant);
            }

            return new GravityFormationResult(
                WasProcessed: true,
                FieldCreated: true,
                FieldMatched: false,
                GravityFieldId: field.FieldId,
                ProximityScore: 0f,
                LifecycleState: field.LifecycleState);
        }
    }
}