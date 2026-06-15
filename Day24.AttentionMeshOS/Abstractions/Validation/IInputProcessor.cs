//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IInputProcessor
    {
        int ExecutionOrder { get; }

        Task ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default);
    }
}