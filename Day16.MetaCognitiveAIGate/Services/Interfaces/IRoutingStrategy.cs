//gs
using Day16.MetaCognitiveAIGate.Models;

namespace Day16.MetaCognitiveAIGate.Services.Interfaces
{
    public interface IRoutingStrategy
    {
        GateRoute Route(GateDecision decision);
    }

}