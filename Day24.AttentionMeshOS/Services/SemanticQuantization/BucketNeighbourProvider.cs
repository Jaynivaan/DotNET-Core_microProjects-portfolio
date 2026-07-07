//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class BucketNeighborProvider
    {
        private readonly ILogger<BucketNeighborProvider> _logger;

        private const int DefaultRadius = 1;

        public BucketNeighborProvider(
            ILogger<BucketNeighborProvider> logger)
        {
            _logger = logger;
        }

        public IReadOnlyList<SemanticBucketKey> GetNeighbors(
            SemanticBucketKey centerKey)
        {
            return GetNeighbors(centerKey, DefaultRadius);
        }

        public IReadOnlyList<SemanticBucketKey> GetNeighbors(
            SemanticBucketKey centerKey,
            int radius)
        {
            if ( radius < 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(radius));
            }

            if ( radius == 0 )
            {
                return Array.Empty<SemanticBucketKey>();
            }

            int totalElements = radius * 2;
            SemanticBucketKey[] neighbors = new SemanticBucketKey[totalElements];
            int writeIndex = 0;

            for ( int offset = -radius; offset <= radius; offset++ )
            {
                if ( offset == 0 )
                {
                    continue;
                }

                unchecked
                {
                    neighbors[writeIndex++] =
                        new SemanticBucketKey(
                            centerKey.BucketCode + offset);
                }
            }

            AemEsgfTelemetry.BucketNeighborExpansionCompleted(
                _logger,
                centerKey.BucketCode,
                neighbors.Length);

            return neighbors;
        }
    }
}