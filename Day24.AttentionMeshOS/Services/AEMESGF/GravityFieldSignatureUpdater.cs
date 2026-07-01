//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFieldSignatureUpdater
    {
        private readonly ILogger<GravityFieldSignatureUpdater> _logger;

        public GravityFieldSignatureUpdater(ILogger<GravityFieldSignatureUpdater> logger) { _logger = logger; }

        public void Update(
            GravityFieldNode field,
            ReadOnlySpan<sbyte> signature,
            GravityOptions options)
        {
            if (!field.IsAllocated)
            {
                return;
            }

            int length = Math.Min(
                signature.Length,
                field.GravityAccumulator.Length);

            length = Math.Min(
                length,
                field.FieldSignature.Length);

            for (int i = 0; i < length; i++)
            {
                int value =
                    field.GravityAccumulator[i] + signature[i];

                if (value > options.MaxGravityInertia)
                {
                    value = options.MaxGravityInertia;
                }

                else if ( value < -options.MaxGravityInertia)
                {
                    value = -options.MaxGravityInertia;
                }
                field.GravityAccumulator[i] = value;

                field.FieldSignature[i] = value > 0
                    ? (sbyte)1
                    : value < 0
                        ? (sbyte)-1
                        : (sbyte)0;
            }

            field.LastEvolvedAt = DateTimeOffset.UtcNow;

            AemEsgfTelemetry.GravityFieldSignatureUpdated(
                _logger,
                field.FieldId);
        }
    }
}