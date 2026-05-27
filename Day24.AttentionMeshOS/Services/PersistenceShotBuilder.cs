//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using System.Collections.Generic;
using System.Text;
namespace Day24.AttentionMeshOS.Services
{
    public sealed class PersistenceShotBuilder : IPersistenceShotBuilder
    {
        public PersistenceShot Build(
            AttentionBall attentionBall,
            IReadOnlyList<Aspiration> aspirations,
            IReadOnlyList<Tendency> tendencies
            )
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Current Aim: {attentionBall.CurrentAim}");
            builder.AppendLine($"Must Not forget : {attentionBall.MustNotForget}");
            builder.AppendLine();

            builder.AppendLine("Aspirations :");

            foreach (var aspiration in aspirations)
                builder.AppendLine($"-{aspiration.Name}");

            builder.AppendLine();

            builder.AppendLine("Tendencies:");

            foreach (var tendency in tendencies)
                builder.AppendLine($"-{tendency.Name}");

            builder.AppendLine();

            builder.AppendLine($"Next Move : {attentionBall.NextMove}");

            return new PersistenceShot(
                builder.ToString(),
                attentionBall,
                aspirations,
                tendencies);

        }
    }
}