//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityDissolutionExecutor: IGravityDissolutionExecutor
    {
        private readonly IGravityRuntime _runtime;
        private readonly IGravityLineageRegistry _lineageRegistry;
        private readonly BucketMaintenanceService _bucketMaintenanceService;

        public GravityDissolutionExecutor(
            IGravityRuntime  runtime,
            IGravityLineageRegistry lineageRegistry,
            BucketMaintenanceService bucketMaintenanceService)
        {
            _runtime = runtime;
            _lineageRegistry = lineageRegistry;
            _bucketMaintenanceService = bucketMaintenanceService;
        }

        public bool Execute (
            GravityFieldNode field,
            DateTimeOffset dissolvedAt)
        {
            ArgumentNullException.ThrowIfNull(field);

            if (!field .IsAllocated)
            {
                return false;
            }

            int runtimeIndex = FindRuntimeIndex(field);

            if (runtimeIndex < 0)
            {
                return false;
            }

            sbyte[] signature = field.FieldSignature.ToArray();

            CandidateFieldRef candidate =
                new CandidateFieldRef(
                    field.FieldId,
                    runtimeIndex);

            _bucketMaintenanceService.RemoveField(
                candidate,
                signature,
                signature);

            _lineageRegistry.RegisterDissolution(
                field.FieldId,
                dissolvedAt);

            return _runtime.ResetField(field.FieldId);

        }
        private int FindRuntimeIndex (
            GravityFieldNode target )
        {
            IReadOnlyList<GravityFieldNode> fields =  _runtime.Fields;

            for ( int i = 0; i < fields.Count; i++ )
            {
                if (ReferenceEquals(fields[i], target))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}