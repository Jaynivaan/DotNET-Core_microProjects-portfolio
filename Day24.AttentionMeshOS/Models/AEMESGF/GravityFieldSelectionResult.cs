//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFieldSelectionResult(
        GravityFieldNode? Field,
        float ProximityScore,
        bool MatchFound
        );
}