//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityRuntime : IGravityRuntime
    {
        private readonly ILogger<GravityRuntime> _logger;
        private readonly GravityOptions _options;
        private readonly object _lockHandle = new();
        private readonly DateTimeOffset _startedAt = DateTimeOffset.UtcNow;

        private int _allocatedFieldCount;
        
        public GravityFieldNode[] Fields { get; }

        public int FieldCount => Fields.Length;

        public TimeSpan Uptime => DateTimeOffset.UtcNow - _startedAt;

        public int AllocatedFieldCount
        {
            get
            {
                lock (_lockHandle)
                {
                    return _allocatedFieldCount;
                }
            }
        }

        public GravityRuntime(
            IOptions<GravityOptions> options,
            ILogger<GravityRuntime> logger
            )
        {
            _options = options.Value;
            _logger = logger;

            Fields = new GravityFieldNode[_options.MaximumGravityFields];

            for ( int i = 0; i < Fields.Length; i++ )
            {
                Fields[i] = new GravityFieldNode(
                    _options.CentroidDimensions,
                    _options.MaxDynamicTagsPerField
                    );
            }

            _logger.LogInformation(
                "AEMESGF Gravity Runtime initialized with {fieldCount} fields.",
                Fields.Length);
        }

        public bool TryAllocateField(out GravityFieldNode? field)
        {
            lock ( _lockHandle)
            {
                for ( int i = 0; i < Fields.Length; ++i )
                {
                    GravityFieldNode Candidate = Fields[i];

                    if (Candidate.IsAllocated)
                    {
                        continue;
                    }

                    InitializeField(Candidate);

                    _allocatedFieldCount++;

                    field = Candidate;

                    _logger.LogInformation(
                        "Gravity Field allocated. FieldId = {FieldId}.",
                        Candidate.FieldId);

                    return true;
                }
            }

            field = null;

            _logger.LogWarning(
                "Gravity field allocation failed.  Runtime slab is full. ");

            return false;
        }

        public bool ResetField(Guid fieldId)
        {
            lock (_lockHandle)
            {
                for ( int i = 0; i < Fields.Length; i++)
                {
                    GravityFieldNode field = Fields[i];

                    if (!field.IsAllocated || field.FieldId != fieldId)
                    {
                        continue;
                    }

                    ResetNode(field);
                    
                    if ( _allocatedFieldCount > 0 )
                    {
                        _allocatedFieldCount--;
                    }

                    _logger.LogInformation(
                        "Gravity Field reset. FieldId={FieldId}.",
                        fieldId);

                    return true;
                }
            }

            return false;
        }

        private static void InitializeField(GravityFieldNode field)
        {
            field.FieldId = Guid.NewGuid();
            field.IsAllocated = true;
            field.LifecycleState = GravityFieldLifecycleState.Dormant;
            field.SemanticMass = 0f;
            field.AttentionEnergy = 0f;
            field.StabilityScore = 0f;
            field.FieldRadius = 0f;

            field.ParticipatingDynamicTagIds.Clear();

            Array.Clear(field.GravityAccumulator);
            Array.Clear(field.FieldSignature);

            field.CreatedAt = DateTimeOffset.UtcNow;
            field.LastEvolvedAt = field.CreatedAt;
        }

        private static void ResetNode(GravityFieldNode field)
        {
            field.FieldId = Guid.NewGuid();
            field.IsAllocated = false;
            field.LifecycleState = GravityFieldLifecycleState.Dormant;
            field.SemanticMass = 0f;
            field.AttentionEnergy = 0f;
            field.StabilityScore = 0f;
            field.FieldRadius = 0f;

            field.ParticipatingDynamicTagIds.Clear();

            Array.Clear(field.GravityAccumulator);
            Array.Clear(field.FieldSignature);

            field.CreatedAt = DateTimeOffset.UtcNow;
            field.LastEvolvedAt = field.CreatedAt;
        }
    }
}