//gs
using System;
namespace Day24.AttentionMeshOS.Models
{
    public sealed class SemanticPhysicsState
    {
        public float AttentionEnergy { get; set; }

        public float Stability { get; set; }

        public float Radius { get; set; }



        public float AttractionPotential { get; set; }

        public float SemanticMomentum { get; set; }



        public float PreviousAttentionEnergy { get; set; }

        public float PreviousStability { get; set; }
        
        public float PreviousRadius { get; set; }




        public DateTimeOffset PreviousUpdatedAt { get; set; }

        public DateTimeOffset LastUpdatedAt { get; set; } = DateTimeOffset.UtcNow;


        public void CapturePrevious(DateTimeOffset timestamp)
        {
            PreviousAttentionEnergy = AttentionEnergy;
            PreviousStability = Stability;
            PreviousRadius = Radius;
            PreviousUpdatedAt = LastUpdatedAt;
            LastUpdatedAt = timestamp;
        }




        public void Reset (DateTimeOffset timestamp )
        {
            AttentionEnergy = 0.0f;
            Stability = 0.0f;
            Radius = 0.0f;

            AttractionPotential = 0.0f;
            SemanticMomentum = 0.0f;

            PreviousAttentionEnergy = 0.0f;
            PreviousStability = 0.0f;
            PreviousRadius = 0.0f;
            
            PreviousUpdatedAt = timestamp;
            LastUpdatedAt = timestamp;
        }


    }
}