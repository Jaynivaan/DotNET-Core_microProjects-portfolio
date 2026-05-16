//gs
using Day16.MetaCognitiveAIGate.Models;

using Day16.MetaCognitiveAIGate.Services.Interfaces;

namespace Day16.MetaCognitiveAIGate.Services.Strategies
{
    public class GeneralRoutingStrategy : IRoutingStrategy
    {
        public GateRoute Route(GateDecision decision)
        {
            //rejected?
            if (!decision.Accepted)
            {
                return new GateRoute
                {
                    Destination = "REJECTED",

                    SendToAiExecution = false,

                    SendToMemory = false,

                    RequiresHumanReview = true
                };
            }

            //accepted prompts
            return new GateRoute
            {
                Destination = "GENERAL_AI_PIPELINE",

                SendToAiExecution = true,

                SendToMemory = decision.AllowMemoryAccess,

                RequiresHumanReview = false
            };
        }
    }
}

//policy based routing strategy..