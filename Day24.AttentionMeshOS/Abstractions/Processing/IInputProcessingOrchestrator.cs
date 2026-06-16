//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface  IInputProcessingOrchestrator
    {
        Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,

            CancellationToken cancellationToken = default);
    }
}