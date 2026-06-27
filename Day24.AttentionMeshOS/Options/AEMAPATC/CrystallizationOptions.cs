//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class CrystallizationOptions
    {
        public bool Enabled { get; set; } = true;

        public int SlotCount { get; set; } = 512;  //this the number of slots from hardware to use for this operation

        public int CentroidDimensions { get; set; } = 64;  //number of signed ternary dimensions maintained..This should allign with semantic memories configured vector size too..

        public float ColdThreshold { get; set; } = 0.75f; //minimum signed ternary resonance needed to get attached to a cold centroid gravityfield

        public float WarmThreshold { get; set; } = 0.82f;  //Resonace threshold required before a centroid is considered as stable

        public int WarmPromotionCount { get; set; } = 3; //number of accumulated signals needed before a centroid is promoted from cold to warm.

        public int HotPromotionCount { get; set; } = 5;  //number of accumulated signals needed before a centroid crystallizes into a dynamic tag birth.

        public int MaxSignalPerInput { get; set; } = 16;  //maximum number of normalized  signals (keywords/tags) considered from a single processing Context.

        public int MaxCentroidInertia { get; set; } = 3;   //Max magnitude allowed for each  centroid accumulator. controls structural inertia and
                                                            //prevents any oscillations caused by  isolated anomalous signals..
    }
}