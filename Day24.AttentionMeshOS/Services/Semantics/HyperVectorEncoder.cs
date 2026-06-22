//gs
using System.Text;
using System.IO.Hashing;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class HyperVectorEncoder : IHyperVectorEncoder
    {
        private readonly ILogger<HyperVectorEncoder> _logger;
        private readonly HyperVectorOptions _options;

        public HyperVectorEncoder(
            ILogger<HyperVectorEncoder> logger,
            IOptions<HyperVectorOptions> options)
        {
            _logger = logger;
            _options = options.Value;


        }

        public HyperVectorPayload Encode(
            Guid attentionBallId,
            VectorPreparationResult vectorPreparation)
        {
            var dimensions = Math.Max(1, _options.Dimensions);

            var sparsity = Math.Clamp(_options.Sparsity, 0.0, 1.0);

            var accumulator = new int[dimensions];

            var tokens = vectorPreparation.Keywords
                .Concat(vectorPreparation.Tags)
                .Where(token => !string.IsNullOrWhiteSpace(token))
                .Select(token => token.Trim().ToLowerInvariant())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var token in tokens)
            {
                AddTokenVector(
                    token,
                    accumulator,
                    sparsity
                    );
            }

            var values = Clip(accumulator);

            var fingerprint = CreateFingerprint(attentionBallId, values);

            _logger.LogInformation(
                "HyperVector encoded for AttentionBall {attentionBallId}. Dimensions = {Dimensions}, Tokens = {TokenCount}, Fingerprint {Fingerprint}.",
                attentionBallId,
                dimensions,
                tokens.Count,
                fingerprint.Fingerprint);

            return new HyperVectorPayload(
                attentionBallId,
                values,
                dimensions,
                fingerprint,
                DateTimeOffset.UtcNow);
                
        }

        private static void AddTokenVector(
            string token,
            int[] accumulator,
            double sparsityThreshold
            )
        {
            var maxByteCount = Encoding.UTF8.GetMaxByteCount(token.Length);

            Span<byte> tokenBytes = maxByteCount <= 512
                ? stackalloc byte[maxByteCount]
                : new byte[maxByteCount];

            var actualByteCount = Encoding.UTF8.GetBytes(
                token,
                tokenBytes);

            var activeTokenSpan = tokenBytes[..actualByteCount];

            for (var i = 0; i < accumulator.Length; i++)
            {
                var hash = XxHash3.HashToUInt64(
                    activeTokenSpan,
                    seed: i);

                var signalByte = (byte)(hash & 0xFF);
                var signByte = (byte)((hash >> 8) & 0xFF);

                if ((signalByte / 255.0) >= sparsityThreshold)
                {
                    accumulator[i] += signByte % 2 == 0
                        ? 1
                        : -1;
                }
            }
        }

        private static float[] Clip(
            int[] accumulator)
        {
            var values = new float[accumulator.Length];

            for (var i = 0; i < accumulator.Length; i++)
            {
                values[i] = accumulator[i] switch
                {
                    > 0 => 1f,
                    < 0 => -1f,
                    _ => 0f
                };
            }
            return values;
        }

        private static SemanticFingerprint CreateFingerprint(
            Guid attentionBallId,
            float[] values)
        {
            Span<byte> bytes = values.Length <= 4096
                ? stackalloc byte[values.Length]
                : new byte[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                bytes[i] = values[i] switch
                {
                    > 0 => 1,
                    < 0 => 2,
                    _ => 0
                };
            }
            var hash = XxHash64.HashToUInt64(bytes);

            return new SemanticFingerprint(
                attentionBallId,
                hash.ToString("x16"));

        }
    }
}