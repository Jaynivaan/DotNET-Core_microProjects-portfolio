//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticMassEngine : ISemanticMassEngine
    {
        private readonly GravityOptions _options;
        private readonly ILogger<SemanticMassEngine> _logger;

        public SemanticMassEngine (
            IOptions<GravityOptions> options,
            ILogger<SemanticMassEngine> logger )
        {
            _logger = logger;
            _options = options.Value;
        }

        public SemanticMassResult UpdateMass(
            GravityFieldNode field,
            GravityFormationContext context,
            float resonanceScore )
        {
            if ( !field.IsAllocated)
            {
                return new SemanticMassResult(0f, 0f, 0f);
            }

            float previousMass = field.SemanticMass;

            float massIncrease =
                _options.BaseParticipationMass +
                (resonanceScore * _options.ResonanceMassWeight) +
                (field.AttentionEnergy * _options.EnergyMassWeight) +
                (field.StabilityScore * _options.StabilityMassWeight);
            
            if ( massIncrease < 0f )
            {
                massIncrease = 0f;
            }

            float currentMass = previousMass + massIncrease;

            if ( currentMass > _options.MaxSemanticMass )
            {
                currentMass = _options.MaxSemanticMass;

                massIncrease = currentMass - previousMass;
            }

            field.SemanticMass = currentMass;

            AemEsgfTelemetry.SemanticMassUpdated(
                _logger,
                field.FieldId,
                previousMass,
                massIncrease,
                currentMass);

            return new SemanticMassResult(
                previousMass,
                massIncrease,
                currentMass
                );

        }
    }
}