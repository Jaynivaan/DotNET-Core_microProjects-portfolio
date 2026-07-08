//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityDissolutionPolicy : IGravityDissolutionPolicy
    {
        private readonly GravityEvolutionOptions _options;

        public GravityDissolutionPolicy(
            IOptions <GravityEvolutionOptions> options )
        {
            _options = options.Value;
        }

        public GravityDissolutionDecision Evaluate (
            GravityDissolutionCandidate candidate)
        {
            if ( !_options.DissolutionEnabled)
            {
                return new GravityDissolutionDecision(false, "Dissolution Disabled.");

            }

            if ( candidate.IsDominantField)
            {
                return new GravityDissolutionDecision(false, "Dormant Field.");
            }

            if ( candidate.RecentlyReinforced)
            {
                return new GravityDissolutionDecision(false, "Recently Reinforced.");
            }

            if (candidate.FieldAge < _options.MinimumFieldAgeForDissolution)
            {
                return new GravityDissolutionDecision(false, "Field too young.");
            }
            
            if (candidate.AttentionEnergy > _options.DissolutionEnergyThreshold)
            {
                return new GravityDissolutionDecision(false, "Attention energy above DissolutionEnergy threshold.");
            }

            if (candidate.Stability > _options.DissolutionStabilityThreshold)
            {
                return new GravityDissolutionDecision(false, "Stability above dissolutionstability threshold.");
            }

            return new GravityDissolutionDecision(true, "Approved for dissolution.");

        }
    }
}