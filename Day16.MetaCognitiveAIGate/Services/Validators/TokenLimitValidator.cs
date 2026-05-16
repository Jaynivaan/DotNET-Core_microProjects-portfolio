//gs

using Day16.MetaCognitiveAIGate.Models;
using Day16.MetaCognitiveAIGate.Services.Interfaces;
using System.ComponentModel;

namespace Day16.MetaCognitiveAIGate.Services.Validators
{
    //protect system from oversized prompts
    public class TokenLimitValidator : IPromptValidator
    {
        //character limit is not so necessary but to keep as one easy validator to validate an attribute.
        private const int MaxPromptLength = 500;

        public GateDecision Validate(PromptInspectionRequest request)
        {
            //empty prompt rejection
            if (string .IsNullOrWhiteSpace (request.Prompt))
            {
                return new GateDecision
                {
                    Accepted = false,

                    Reason = "Prompt is Empty",

                    Category = "EMPTY_PROMPT",

                    AllowMemoryAccess = false
                };
            }

            //prompt length protection
            if (request.Prompt.Length > MaxPromptLength)
            {
                return new GateDecision
                {
                    Accepted = false,

                    Reason = "Prompt exeeds allowed token limit",

                    Category = " TOKEN_LIMIT",

                    AllowMemoryAccess = false
                };
            }
            //later if needed addable validators as next files are 
            //
            //-Memory access validator
            //= dangerous prompt validator
            //Jailbreak validator
            //p.injection validator
            //Emotion risk validator
            //privacy risk validator.
            //safe decision
            //this is just prompt length validator onthis tokenLimitvalidator.
            return new GateDecision
            {
                Accepted = true,

                Reason = "Prompt passed token inspection. congrats..",

                Category = "SAFE",

                AllowMemoryAccess = request.WantsMemoryAccess
            };
        }
    }
}