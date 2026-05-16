//gs
namespace Day16.MetaCognitiveAIGate.Models
{
    //this the api response wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public T? Data { get; set; }

        public GateMetadata? Metadata { get; set; }
    }
}