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
        private readonly GravityFieldNode[] _fields;

        private int _allocatedFieldCount;

        public IReadOnlyList<GravityFieldNode> Fields => _fields;

        public int FieldCount => _fields.Length;

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

            _fields = new GravityFieldNode[_options.MaximumGravityFields];

            for ( int i = 0; i < _fields.Length; i++ )
            {
                _fields[i] = new GravityFieldNode(
                    _options.CentroidDimensions,
                    _options.MaxDynamicTagsPerField );
            }

            AemEsgfTelemetry.GravityRuntimeInitialized(
                _logger,
                _fields.Length);
        }

        public bool TryAllocateField(out GravityFieldNode? field)
        {
            lock ( _lockHandle)
            {
                for ( int i = 0; i < _fields.Length; i++ )
                {
                    GravityFieldNode candidate = _fields[i];

                    if (candidate.IsAllocated)
                    {
                        continue;
                    }

                    InitializeField(candidate);

                    _allocatedFieldCount++;

                    field = candidate;

                    AemEsgfTelemetry.GravityFieldAllocated(
                        _logger,
                        candidate.FieldId);

                    return true;
                }
            }

            field = null;

            AemEsgfTelemetry.GravityRuntimeFull(_logger);

            return false;
        }

        public bool ResetField(Guid fieldId)
        {
            lock (_lockHandle)
            {
                for ( int i = 0; i < _fields.Length; i++)
                {
                    GravityFieldNode field = _fields[i];

                    if (!field.IsAllocated || field.FieldId != fieldId)
                    {
                        continue;
                    }

                    ResetNode(field);
                    
                    if ( _allocatedFieldCount > 0 )
                    {
                        _allocatedFieldCount--;
                    }

                    AemEsgfTelemetry.GravityFieldReset(
                        _logger,
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

            field.Participations.Clear();

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

            field.Participations.Clear();

            Array.Clear(field.GravityAccumulator);
            Array.Clear(field.FieldSignature);

            field.CreatedAt = DateTimeOffset.UtcNow;
            field.LastEvolvedAt = field.CreatedAt;
        }
    }
}