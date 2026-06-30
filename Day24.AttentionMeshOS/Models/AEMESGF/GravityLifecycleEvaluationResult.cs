//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityLifecycleEvaluationResult(
        bool StateChanged,
        GravityFieldLifecycleState PreviousState,
        GravityFieldLifecycleState CurrentState
        );
}