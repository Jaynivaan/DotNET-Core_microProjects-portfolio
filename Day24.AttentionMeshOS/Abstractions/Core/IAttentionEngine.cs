//gs

using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionEngine
    {
        Task<AttentionProcessResult> ProcessAsync(
            string userInput,
            CancellationToken cancellationToken = default 
            );
    }
}