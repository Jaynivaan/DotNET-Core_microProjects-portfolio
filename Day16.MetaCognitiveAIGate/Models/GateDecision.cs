//gs

using System;

namespace Day16.MetaCognitiveAIGate.Models
{
    //judgement model object
    public class GateDecision
    {
        //overall result
        public bool Accepted { get; set; }

        //why this decision happened
        public string Reason { get; set; } = "";

        //danger category or the observation category
        public string Category { get; set; } = "";

        //whether memory systems are allowed
        public bool AllowMemoryAccess { get; set; }

        //the time when decision was produced
        public DateTime DecidedAt { get; set; } = DateTime.UtcNow;

    }
}