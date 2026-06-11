//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class AttentionReleaseOptions
    {
        public double MaximumReleaseWeight { get; set; } = 0.2;

        public double MaximumVelocityPerHour { get; set; } = 0.01;

        public bool AllowAnchorRelease { get; set; } = false;
    }
}