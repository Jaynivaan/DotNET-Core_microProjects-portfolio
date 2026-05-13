//gs

namespace Day13.LocalAiPingAPI.Models
{
    //incoming user prompt request
    public class AiRequest
    {
        //prompt sent to local ai
        public string Prompt { get; set; } = "";

        //per request level system instruction
        public string? SystemInstruction { get; set; } 

    }
}