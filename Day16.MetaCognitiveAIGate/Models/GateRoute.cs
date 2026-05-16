//gs 

namespace Day16.MetaCognitiveAIGate.Models
{

    //the model part associated with where the accepted or rejected prompt to go next
    public class GateRoute
    {
        public string Destination { get; set; } = "";

        public bool SendToMemory { get; set; }

        public bool SendToAiExecution { get; set; }//submit to llm

        public bool RequiresHumanReview { get; set; }

    }
}