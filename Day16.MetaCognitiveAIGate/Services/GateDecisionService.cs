//gs

using Day16.MetaCognitiveAIGate.Models;
using Day16.MetaCognitiveAIGate.Services.Interfaces;
using System.Collections;

namespace Day16.MetaCognitiveAIGate.Services
{
    public class GateDecisionService : IGateDecisionService
    {
        private readonly IEnumerable<IPromptValidator> _validators;

        public GateDecisionService (IEnumerable<IPromptValidator> validators)
        {
            _validators = validators;
        }
        public GateDecision Inspect(PromptInspectionRequest request)
        {
            foreach (var validator in _validators)
            {
                var decision = validator.Validate(request);

                if (!decision .Accepted)
                {
                    return decision;
                }
            }
            return new GateDecision
            {
                Accepted = true,

                Reason = "Prompt passes all inspections",

                Category = "SAFE",

                AllowMemoryAccess = request.WantsMemoryAccess
            };
        }
    }
}