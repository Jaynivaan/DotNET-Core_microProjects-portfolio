//gs

using Day16.MetaCognitiveAIGate.Models;

namespace Day16.MetaCognitiveAIGate.Services.Interfaces
{
    //main gate service contract like cheif of validators..
    public interface IGateDecisionService
    {
        GateDecision Inspect(PromptInspectionRequest request);
    }
}