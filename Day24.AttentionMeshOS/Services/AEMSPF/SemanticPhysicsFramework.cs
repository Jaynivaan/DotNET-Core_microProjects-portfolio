//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticPhysicsFramework : ISemanticPhysicsFramework
    {
        private readonly IReadOnlyList<ISemanticPhysicsLaw> _laws;

        public SemanticPhysicsFramework(IEnumerable<ISemanticPhysicsLaw> laws)
        {
            _laws = laws.ToList();
        }
        
        public SemanticPhysicsResult Evaluate(SemanticPhysicsContext context)
        {
           SemanticPhysicsResult result = 
                SemanticPhysicsResult.FromState(context.CurrentState);

            for (int i = 0; i < _laws.Count; i++)
            {
                ISemanticPhysicsLaw law = _laws[i];
                result = law.Evaluate(context, result);
            }

            return result;
        }
    }
}