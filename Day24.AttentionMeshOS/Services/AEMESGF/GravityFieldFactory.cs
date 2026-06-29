//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFieldFactory
    {
        private readonly IGravityRuntime _runtime;
        private readonly IGravityRegistry _registry;
        private readonly GravityMembershipManager _membershipManager;
        private readonly GravityFieldSignatureUpdater _signatureUpdater;
        private readonly GravityOptions _options;

        public GravityFieldFactory(
            IGravityRuntime runtime,
            IGravityRegistry registry,
            GravityMembershipManager membershipManager,
            GravityFieldSignatureUpdater signatureUpdater,
            IOptions<GravityOptions> options )
        {
            _runtime = runtime;
            _registry = registry;
            _membershipManager = membershipManager;
            _signatureUpdater = signatureUpdater;
            _options = options.Value;
        }

        public GravityFieldNode? Create(
            GravityFormationContext context)
        {
            if ( !_runtime.TryAllocateField(out GravityFieldNode? field)  ||
                field is null )
            {
                return null;
            }

            DateTimeOffset now = DateTimeOffset.UtcNow;

            field.LifecycleState = GravityFieldLifecycleState.Emerging;
            field.SemanticMass = _options.BaseParticipationMass;
            field.CreatedAt = now;
            field.LastEvolvedAt = now;

            _membershipManager.AddParticipant(
                field,
                context.DynamicTagId,
                _options);

            _signatureUpdater.Update(
                field,
                context.TernarySignature,
                _options);

            _registry.Register(
                new GravityFieldRecord(
                    field.FieldId,
                    context.DisplayName,
                    field.CreatedAt));

            return field;
        }
    }
}