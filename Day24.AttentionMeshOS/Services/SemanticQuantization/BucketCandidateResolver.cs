//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class BucketCandidateResolver : ICandidateResolver
    {
        private const int NeighborRadius = 1;

        private readonly ISemanticQuantizer _quantizer;
        private readonly IBucketRegistry _bucketRegistry;
        private readonly BucketNeighborProvider _neighborProvider;
        private readonly BucketCandidateOrderingService _orderingService;

        public string Name => "Bucket";

        public BucketCandidateResolver(
            ISemanticQuantizer quantizer,
            IBucketRegistry bucketRegistry,
            BucketNeighborProvider neighborProvider,
            BucketCandidateOrderingService orderingService)
        {
            _quantizer = quantizer;
            _bucketRegistry = bucketRegistry;
            _neighborProvider = neighborProvider;
            _orderingService = orderingService;
        }

        public CandidateResolutionResult Resolve(
            CandidateResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            SemanticBucketKey centerKey =
                _quantizer.Quantize(
                    context.IncomingSignature,
                    context.PresenceMask);

            List<BucketCandidateOrderingInput> candidatePool = new();

            AddBucketCandidates(
                centerKey,
                0,
                candidatePool);

            IReadOnlyList<SemanticBucketKey> neighbors =
                _neighborProvider.GetNeighbors(
                    centerKey,
                    NeighborRadius);

            for (int i = 0; i < neighbors.Count; i++)
            {
                AddBucketCandidates(
                    neighbors[i],
                    1,
                    candidatePool);
            }

            IReadOnlyList<CandidateFieldRef> orderedCandidates =
                _orderingService.Order(candidatePool);

            return new CandidateResolutionResult(
                orderedCandidates,
                orderedCandidates.Count,
                UsedFallback: false,
                ResolverName: Name);
        }

        private void AddBucketCandidates(
            SemanticBucketKey bucketKey,
            int bucketDistance,
            List<BucketCandidateOrderingInput> candidatePool)
        {
            if ( !_bucketRegistry.TryGetEntries(
                bucketKey,
                out IReadOnlyList<SemanticBucketEntry> entries))
            {
                return;
            }

            for ( int i = 0; i < entries.Count; i++ )
            {
                candidatePool.Add(
                    new BucketCandidateOrderingInput(
                        entries[i].Candidate,
                        bucketDistance,
                        FingerprintSimilarity: 0));
            }
        }
    }
}