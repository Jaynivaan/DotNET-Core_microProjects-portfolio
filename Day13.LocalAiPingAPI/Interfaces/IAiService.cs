//gs

using Day13.LocalAiPingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day13.LocalAiPingAPI.Interfaces
{
    //
    public interface IAiService
    {
        //normal response
        Task<AiResponseDto> GenerateAsync(AiRequest request);

        //streaming response
        IAsyncEnumerable<string> StreamAsync(AiRequest request);
    }
}