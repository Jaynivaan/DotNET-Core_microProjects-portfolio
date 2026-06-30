//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityRuntimeAggregator
    {
        private readonly IGravityRuntime _runtime;

        public GravityRuntimeAggregator(IGravityRuntime runtime)
        {
            _runtime = runtime;
        }

        public GravityRuntimeAggregate CalculateMetrics()
        {
            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;

            int count = 0;
            float totalMass = 0f;
            int activeMemberships = 0;
            HashSet<Guid> uniqueDynamicTagIds = new();
            float stabilitySum = 0f;
            float radiusSum = 0f;

            int largestCount = 0;
            Guid? newestFieldId = null;
            Guid? strongestFieldId = null;

            DateTimeOffset newestTime = DateTimeOffset.MinValue;
            float maxMass = -1f;

            for (int i = 0; i < fields.Count; i++)
            {
                GravityFieldNode field = fields[i];

                if ( !field.IsAllocated)
                {
                    continue;
                }

                count++;
                totalMass += field.SemanticMass;

                int currentMembers = field.Participations.Count;
                activeMemberships += currentMembers;

                foreach (var participation in  field.Participations.Values)
                {
                    uniqueDynamicTagIds.Add(participation.DynamicTagId);
                }

                stabilitySum += field.StabilityScore;
                radiusSum += field.FieldRadius;

                if ( currentMembers > largestCount )
                {
                    largestCount = currentMembers;
                }

                if ( field.CreatedAt > newestTime )
                {
                    newestTime = field.CreatedAt;
                    newestFieldId = field.FieldId;
                }

                if ( field.SemanticMass > maxMass )
                {
                    maxMass = field.SemanticMass;
                    strongestFieldId = field.FieldId;
                }
            }

            double averageMembershipsPerDynamicTag = 
                uniqueDynamicTagIds.Count > 0
                    ? (double)activeMemberships / uniqueDynamicTagIds.Count
                    : 0;

            return new GravityRuntimeAggregate(
                count,
                totalMass,
                activeMemberships,
                largestCount,
                newestFieldId,
                strongestFieldId,
                count > 0 ? stabilitySum / count : 0f,
                count > 0 ? radiusSum / count : 0f,
                averageMembershipsPerDynamicTag);
        }
    }
}