//gs 

using System;

namespace Day16.MetaCognitiveAIGate.Models
{
    //the metadata is the extra observations on the happening s  at the gate events.

    public class GateMetadata
    {
        public DateTime InspectedAt { get; set; } = DateTime.UtcNow;

        public int PromptLength { get; set; }

        public string Source { get; set; } = "";

        public string PipelineVersion { get; set; } = "v1";


    }
}