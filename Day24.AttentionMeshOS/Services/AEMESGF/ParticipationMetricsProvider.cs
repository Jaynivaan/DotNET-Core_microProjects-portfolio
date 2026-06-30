//gs
using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Services
{
    public sealed class ParticipationMetricsProvider
    {
        public ParticipationMetrics GetMetrics (GravityFieldNode field)
        {
            int totalParticipations =  field.Participations.Count;

            if ( totalParticipations == 0 )
            {
                return new ParticipationMetrics(0, 0d, 0);

            }

            int reinforcementTotal = 0;
            int highestReinforcement = 0;

            foreach ( DynamicTagParticipation participation in field.Participations .Values)
            {
                int count = participation.ReinforcementCount;

                reinforcementTotal += count;

                if ( count > highestReinforcement)
                {
                    highestReinforcement = count;
                }

            }

            return new ParticipationMetrics(
                totalParticipations,
                (double)reinforcementTotal / totalParticipations,
                highestReinforcement);
        }
    }
}