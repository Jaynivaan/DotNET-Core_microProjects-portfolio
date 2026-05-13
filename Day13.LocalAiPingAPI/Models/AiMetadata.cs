//gs
using System;

namespace Day13.LocalAiPingAPI.Models
{

    //metadata helps debugging and observability

    public class AiMetadata
    {
        //time response generated
        public DateTime GeneratedAt { get; set; } 
        //prompt size
        public int PromptLength { get; set; }

        //provider used
        public string Provider { get; set; } = "";
    }
}