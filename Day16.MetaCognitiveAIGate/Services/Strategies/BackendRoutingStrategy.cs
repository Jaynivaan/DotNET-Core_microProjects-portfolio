//gs

using Day16.MetaCognitiveAIGate.Models;
using Day16.MetaCognitiveAIGate.Services.Interfaces;

namespace Day16.MetaCognitiveAIGate.Services.Strategies
{
    public class BackendRoutingStrategy : IRoutingStrategy
    {
        public GateRoute Route(GateDecision decision)
        {

            //rejected prompts
            if(!decision.Accepted)
            {
                return new GateRoute
                {
                    Destination = "BACKEND_REJECTION_QUEUE",

                    SendToAiExecution = false,

                    SendToMemory = false,

                    RequiresHumanReview = true

                };
            }

            return new GateRoute
            {
                Destination = "BACKEND_AI_PIPELINE",

                SendToAiExecution = true,

                SendToMemory = decision.AllowMemoryAccess,

                RequiresHumanReview = false
            };
        }
    }
}