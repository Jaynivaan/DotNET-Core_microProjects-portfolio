//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticPhysicsResult(
        float AttentionEnergy,
        float Stability,
        float Radius,
        float AttractionPotential,
        float SemanticMomentum
        )
    {
        public static SemanticPhysicsResult FromState(SemanticPhysicsState state)
        {
            return new SemanticPhysicsResult(
                state.AttentionEnergy,
                state.Stability,
                state.Radius,
                state.AttractionPotential,
                state.SemanticMomentum
                );
        }
    }
}