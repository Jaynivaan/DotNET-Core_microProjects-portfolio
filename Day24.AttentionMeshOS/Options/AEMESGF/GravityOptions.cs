//gs

using System.ComponentModel.DataAnnotations;
///defines the config for the emergent Semantic gravity field subsystem.
namespace Day24.AttentionMeshOS.Options
{
    public sealed class GravityOptions
    {

        public bool Enabled { get; set; } = true;

        [Range(1, 4096)]
        public int MaximumGravityFields { get; set; } = 1024;

        [Range(1, 4096)]
        public int CentroidDimensions { get; set; } = 128;

        [Range(1, 1024)]
        public int MaxDynamicTagsPerField { get; set; } = 64;

        [Range(0.0f, 1.0f)]
        public float FieldFormationThreshold { get; set; } = 0.70f;

        [Range(0.0f, 1.0f)]
        public float ResonanceThreshold { get; set; } = 0.65f;

        [Range(0.0f, 1.0f)]
        public float MaximumFieldRadius { get; set; } = 1.0f;

        [Range(0.0f, 1.0f)]
        public float MergeThreshold { get; set; } = 0.90f;

        [Range(0.0f, 1.0f)]
        public float StabilityThreshold { get; set; } = 0.75f;

        [Range(0.0f, 1.0f)]
        public float SignedTernaryWeight { get; set; } = 0.70f;

        [Range(0.0f, 1.0f)]
        public float VocabularyWeight { get; set; } = 0.30f;

        [Range(0.0f, 100.0f)]
        public float BaseParticipationMass { get; set; } = 1.0f;

        [Range(0.0f, 100.0f)]
        public float ResonanceMassWeight { get; set; } = 1.0f;

        [Range(0.0f, 100.0f)]
        public float EnergyMassWeight { get; set; } = 0.50f;

        [Range(0.0f, 100.0f)]
        public float StabilityMassWeight { get; set; } = 0.50f;

        [Range(1, 100)]
        public int MaxGravityInertia { get; set; } = 3;
    }
}