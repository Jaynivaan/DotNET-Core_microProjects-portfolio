//gs
namespace Day13.LocalAiPingAPI.Models
{
    //structured ai response returned to client
    public class AiResponseDto
    {
        //ai  generated text
        public string Response { get; set; } = "";

        //which model generated response
        public string Model { get; set; } = "";

    }
}