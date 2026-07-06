//gs
using System.Collections.Generic;

using Day24.AttentionMeshOS.Models;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IBucketRegistry
    {
        void Register(
            SemanticBucketKey bucketKey,
            SemanticBucketEntry entry);

        bool UnRegister(
            SemanticBucketKey bucketKey,
            CandidateFieldRef candidate);

        bool TryGetEntries(
            SemanticBucketKey bucketKey,
            out IReadOnlyList<SemanticBucketEntry> entries);

        IReadOnlyList<SemanticBucketSnapshot> GetSnapshots();

        void Clear();
    }
}