//gs

namespace Day16.MetaCognitiveAIGate.Models
{
    //this is the model for incoming prompt object.
    // this represents the thought entering the gate out of no where this appears
    public class PromptInspectionRequest
    {
        public string Prompt { get; set; } = "";                    //what is this ?

        public string Source { get; set; } = "unknown";             // where did it come from?

        public bool WantsMemoryAccess { get; set; }                 // is it trying to touch memory?
    }
}
