//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{

    public readonly record struct BucketCandidateOrderingInput(
        CandidateFieldRef Candidate,
        int BucketDistance,
        int FingerprintSimilarity);

    public sealed class BucketCandidateOrderingService
    {
        public IReadOnlyList<CandidateFieldRef> Order(
            IReadOnlyList<BucketCandidateOrderingInput> candidates)
        {
            ArgumentNullException.ThrowIfNull(candidates);

            if ( candidates.Count == 0)
            {
                return Array.Empty<CandidateFieldRef>();

            }

            List<BucketCandidateOrderingInput> sortableList =
                new(candidates);

            sortableList.Sort((left, right) =>
            {
                int distanceCompare =
                left.BucketDistance.CompareTo(right.BucketDistance);

                if (distanceCompare != 0)
                {
                    return distanceCompare;
                }

                int fingerprintCompare = 
                    right.FingerprintSimilarity.CompareTo(left.FingerprintSimilarity);

                if (fingerprintCompare != 0)
                {
                    return fingerprintCompare;
                }


                int indexCompare =
                    left.Candidate.RuntimeIndex.CompareTo(
                        right.Candidate.RuntimeIndex);
                if (indexCompare != 0)
                {
                    return indexCompare;
                }

                return left.Candidate.FieldId.CompareTo(
                    right.Candidate.FieldId);
            });
            CandidateFieldRef[] result = 
                new CandidateFieldRef[sortableList.Count];

            for ( int i = 0; i < sortableList.Count; i++ )
            {
                result[i] = sortableList[i].Candidate;
            }

            return result;
        }

    }
}