//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionProcessResult(
        bool IsSuccess,
        
        AttentionResponse? Response,
        
        InvalidInputResponse? InvalidInputResponse
        );
}