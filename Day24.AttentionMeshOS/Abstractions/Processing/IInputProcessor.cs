//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IInputProcessor
    {
        int ExecutionOrder { get; }

        bool IsCritical { get; }

        Task <ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default);
    }
}