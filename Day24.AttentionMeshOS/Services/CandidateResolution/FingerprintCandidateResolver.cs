//gs

using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class FingerprintCandidateResolver    :   ICandidateResolver 
    {
        private readonly CandidateOrderingService _orderingService;
        private readonly CandidateResolutionMetricsProvider _metricsProvider;
        private readonly ILogger<FingerprintCandidateResolver> _logger;
        private readonly IGravityRuntime _runtime;
        private readonly GravityFieldCandidateFingerprintProvider _provider;
        private readonly CandidateFingerprintBuilder _builder;
        private readonly CandidateResolutionOptions _options;

        public string Name => "Fingerprint";

        public FingerprintCandidateResolver(
            CandidateOrderingService orderingService,
            CandidateResolutionMetricsProvider metricsProvider,
            ILogger<FingerprintCandidateResolver> logger,
            IGravityRuntime runtime,
            GravityFieldCandidateFingerprintProvider provider,
            CandidateFingerprintBuilder builder,
            IOptions<CandidateResolutionOptions> options)
        {
            _orderingService = orderingService;
            _metricsProvider= metricsProvider;
            _logger = logger;
            _runtime = runtime;
            _provider = provider;
            _builder = builder;
            _options = options.Value;
        }

        public CandidateResolutionResult Resolve(
            CandidateResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            AemEsgfTelemetry.CandidateResolutionStarted(
                    _logger,
                    Name);

            CandidateFingerprint incoming =
                _builder.Build(
                    context.IncomingSignature,
                    context.PresenceMask,
                    _options.FingerprintBlockSize);


            List<(CandidateFieldRef Candidate, int MatchStrength)> candidatePool = new();
           //ist<CandidateFieldRef> candidates = new();

            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;


            for ( int i = 0; i < fields.Count; i++ )
            {
                GravityFieldNode field = fields[i];

                if ( !field .IsAllocated )
                {
                    continue;
                }

                CandidateFingerprint fieldFingerprint =
                    _provider.Create(field);

                if ( IsMatch(incoming, fieldFingerprint))
                {
                    // candidates.Add( new CandidateFieldRef( field.FieldId, i));
                    candidatePool.Add(
                        (new CandidateFieldRef(field.FieldId, i), incoming.BlockCount));
                }
            }

            IReadOnlyList<CandidateFieldRef> orderedCandidates =
                _orderingService.Order(candidatePool);

            if ( orderedCandidates.Count < _options.MinimumCandidateCount &&
                _options.AllowFallbackToAllFields)
            {
                List<CandidateFieldRef> fallbackCandidates = new();

                for ( int i = 0; i < fields.Count; i++)
                {
                    GravityFieldNode field = fields[i];

                    if (!field .IsAllocated )
                    {
                        continue;
                    }

                    //candidates.Add( new CandidateFieldRef(  field.FieldId, i));
                    fallbackCandidates.Add(
                         new CandidateFieldRef(field.FieldId, i));
                }

                AemEsgfTelemetry.CandidateFallbackUsed(
                    _logger,
                    Name);

                AemEsgfTelemetry.CandidateResolutionCompleted(
                    _logger,
                    Name,
                    fallbackCandidates.Count,
                    true);

                _metricsProvider.Record(
                    Name,
                    fallbackCandidates.Count,
                    true);

                return new CandidateResolutionResult(
                    fallbackCandidates,
                    fallbackCandidates.Count,
                    UsedFallback: true,
                    ResolverName: Name);
            }

            AemEsgfTelemetry.CandidateResolutionCompleted(
                    _logger,
                    Name,
                    orderedCandidates.Count,
                    false);

            _metricsProvider.Record(
                Name,
                orderedCandidates.Count,         
                false);            

            return new CandidateResolutionResult(
                orderedCandidates,
                orderedCandidates.Count,
                UsedFallback: false,
                ResolverName: Name);
        }

        private static bool IsMatch(
            CandidateFingerprint left,
            CandidateFingerprint right)
        {
            if ( left.BlockCount != right.BlockCount )
            {
                return false;
            }

            for ( int i = 0; i < left.BlockCount; i++ )
            {
                if (left.BlockCodes[i] != right.BlockCodes[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}