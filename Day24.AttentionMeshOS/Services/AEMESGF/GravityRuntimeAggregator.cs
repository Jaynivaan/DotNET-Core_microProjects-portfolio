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
            
            
            //spf - sums
            float energySum = 0f;
            float potentialSum = 0f;
            float momentumSum = 0f;

            int largestCount = 0;
            Guid? newestFieldId = null;
            Guid? strongestFieldId = null;
            //spf-fieldTrackers
            Guid? highestPotentialFieldId = null;
            Guid? fastestRisingFieldId = null;
            Guid? weakestActiveFieldId = null;


            DateTimeOffset newestTime = DateTimeOffset.MinValue;
            float maxMass = -1f;

            //spf- extrema
            float maxPotential = -1f;
            float maxMomentum = -1f;
            float minPotential = float.MaxValue;
            float maxEnergy = 0f;


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

                stabilitySum += field.Physics.Stability;
                radiusSum += field.Physics.Radius;


                //spf aggregation
                energySum += field.Physics.AttentionEnergy;
                potentialSum += field.Physics.AttractionPotential;
                momentumSum += field.Physics.SemanticMomentum;


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

                //Highest attractionPotential field (spf)
                if ( field.Physics.AttractionPotential > maxPotential )
                {
                    maxPotential = field.Physics.AttractionPotential;
                    highestPotentialFieldId = field.FieldId;
                }

                //fastest rising field
                if ( field.Physics.SemanticMomentum > maxMomentum )
                {
                    maxMomentum = field.Physics.SemanticMomentum;
                    fastestRisingFieldId = field.FieldId;
                }
                
                //weakest active field by potential
                if (field.Physics.AttractionPotential < minPotential)
                {
                    minPotential = field.Physics.AttractionPotential;
                    weakestActiveFieldId = field.FieldId;
                }

                //Highest energy observed
                if ( field.Physics.AttentionEnergy > maxEnergy )
                {
                    maxEnergy = field.Physics.AttentionEnergy;
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
                averageMembershipsPerDynamicTag,
                
                //spf aggregate values
                count > 0 ? energySum / count : 0f,
                count > 0 ? potentialSum / count : 0f,
                count > 0 ? momentumSum / count : 0f,
                highestPotentialFieldId,
                fastestRisingFieldId,
                weakestActiveFieldId,
                maxEnergy,
                maxPotential < 0f ? 0f : maxPotential,
                maxMomentum < 0f ? 0f : maxMomentum
                );
        }
    }
}