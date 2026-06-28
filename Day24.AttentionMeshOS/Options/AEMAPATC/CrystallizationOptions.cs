//gs
using System.ComponentModel.DataAnnotations;

namespace Day24.AttentionMeshOS.Options
{
    public sealed class CrystallizationOptions
    {
        public bool Enabled { get; set; } = true;

        [Range(1, 4096)] 
        public int SlotCount { get; set; } = 512;  //this the number of slots from hardware to use for this operation

        [Range(1, 4096)]
        public int CentroidDimensions { get; set; } = 64;  //number of signed ternary dimensions maintained..This should allign with semantic memories configured vector size too..

        [Range(0.0f, 1.0f)]
        public float ColdThreshold { get; set; } = 0.75f; //minimum signed ternary resonance needed to get attached to a cold centroid gravityfield

        [Range(0.0f, 1.0f)]
        public float WarmThreshold { get; set; } = 0.82f;  //Resonace threshold required before a centroid is considered as stable

        [Range(1, 100)]
        public int WarmPromotionCount { get; set; } = 3; //number of accumulated signals needed before a centroid is promoted from cold to warm.

        [Range(1, 100)]
        public int HotPromotionCount { get; set; } = 5;  //number of accumulated signals needed before a centroid crystallizes into a dynamic tag birth.

        [Range(1, 1000)]
        public int MaxSignalPerInput { get; set; } = 16;  //maximum number of normalized  signals (keywords/tags) considered from a single processing Context.

        [Range(1, 100)]
        public int MaxCentroidInertia { get; set; } = 3;   //Max magnitude allowed for each  centroid accumulator. controls structural inertia and
                                                            //prevents any oscillations caused by  isolated anomalous signals..
    }
}