//gs
using System.ComponentModel.DataAnnotations;

namespace Day24.AttentionMeshOS.Options
{
    public sealed class SemanticPhysicsOptions
    {
        //============================
        //AttentionEnergy
        //============================
        [Range(0.0f, 10.0f)]
        public float EnergyReinforcementRate { get; set; } = 1.0f;

        [Range(0.0f, 100.0f)]
        public float MinimumEnergy { get; set; } = 0.1f;

        [Range(0.0f, 100.0f)]
        public float MaximumEnergy { get; set; } = 100.0f;

        //============================
        //Stability
        //============================

        [Range(0.0f, 10.0f)]
        public float StabilityReinforcementRate { get; set; } = 1.0f;

        [Range(0.0f, 1.0f)]
        public float MinimumStability { get; set; } = 0.0f;

        [Range(0.0f, 1.0f)]
        public float MaximumStability { get; set; } = 1.0f;

        //============================
        //Radius
        //============================
        [Range(0.0f, 10.0f)]
        public float RadiusExpansionRate { get; set; } = 1.0f;

        [Range(0.0f, 100.0f)]
        public float MinimumRadius { get; set; } = 0.1f;

        [Range(0.0f, 100.0f)]
        public float MaximumRadius { get; set; } = 100.0f;

        //============================
        //AttractionPotential
        //============================

        [Range(0.0f, 10.0f)]
        public float PotentialMassWeight { get; set; } = 0.4f;

        [Range(0.0f, 10.0f)]
        public float PotentialEnergyWeight { get; set; } = 0.3f;

        [Range(0.0f, 10.0f)]
        public float PotentialStabilityWeight { get; set; } = 0.3f;

        [Range(0.0f, 10.0f)]
        public float PotentialRadiusPenalty { get; set; } = 0.1f;

        //============================
        //SemanticMomentum
        //============================

        [Range(0.0f, 10.0f)]
        public float MomentumSensitivity { get; set; } = 0.5f;

    }
}