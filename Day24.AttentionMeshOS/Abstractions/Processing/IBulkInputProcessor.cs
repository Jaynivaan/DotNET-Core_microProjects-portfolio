//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IBulkInputProcessor
    {
        Task<BulkInputResponse> ProcessAsync (
            BulkInputRequest request,
            CancellationToken cancellationToken = default);
        
    }
}