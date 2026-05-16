//gs
using Day16.MetaCognitiveAIGate.Models;

namespace Day16.MetaCognitiveAIGate.Services.Interfaces
{
    //validator contract 
    public interface IPromptValidator
    {
        GateDecision Validate(PromptInspectionRequest request);
    }
}