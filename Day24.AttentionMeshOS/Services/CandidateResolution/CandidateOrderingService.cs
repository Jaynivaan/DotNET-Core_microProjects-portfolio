//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateOrderingService
    {
        private readonly ILogger<CandidateOrderingService> _logger;
        private readonly CandidateResolutionOptions _options;

        public CandidateOrderingService (
            IOptions<CandidateResolutionOptions> options ,
            ILogger<CandidateOrderingService> logger )
        {
            _logger = logger;
            _options = options.Value;
        }

        public IReadOnlyList<CandidateFieldRef> Order(
            List<(CandidateFieldRef Candidate, int MatchStrength)> candidatePool)
        {
            ArgumentNullException.ThrowIfNull(candidatePool);

            candidatePool.Sort((left, right) =>
            {
                int matchComparison =
                    right.MatchStrength.CompareTo(left.MatchStrength);

                if (matchComparison != 0)
                {
                    return matchComparison;
                }

                return left.Candidate.FieldId.CompareTo(
                    right.Candidate.FieldId);

            });
            if ( candidatePool.Count >  _options.MaximumCandidateCount)
            {
                AemEsgfTelemetry.CandidateCountBounded(
                    _logger,
                    "CandidateOrderingService",
                    candidatePool.Count,
                    _options.MaximumCandidateCount);
            }

            int finalCount = Math.Min(
                candidatePool.Count,
                _options.MaximumCandidateCount);

            CandidateFieldRef[] result = new CandidateFieldRef[finalCount];

            for ( int i = 0; i < finalCount; i++ )
            {
                result[i] = candidatePool[i].Candidate;
            }
            return result;
        }
    }
}
