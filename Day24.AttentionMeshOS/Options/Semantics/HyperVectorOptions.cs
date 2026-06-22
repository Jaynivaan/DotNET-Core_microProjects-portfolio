//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class HyperVectorOptions
    {
        public bool Enabled { get; set; } = true;

        public int Dimensions { get; set; } = 4096;

        public double Sparsity { get; set; } = 0.67;
    }
}