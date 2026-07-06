//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class BucketNeighborProvider
    {
        private const int DefaultRadius = 1;

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
            return neighbors;
        }
    }
}