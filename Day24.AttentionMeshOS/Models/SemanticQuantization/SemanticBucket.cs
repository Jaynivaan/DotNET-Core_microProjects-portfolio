//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed class SemanticBucket
    {
        public SemanticBucket (
            SemanticBucketKey bucketKey)
        {
            BucketKey = bucketKey;
        }

        public SemanticBucketKey BucketKey { get; }

        internal List<SemanticBucketEntry> InternalEntries { get; } = new();
    }
}