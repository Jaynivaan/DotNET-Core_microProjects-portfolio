//gs

using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class FingerprintCandidateResolver    :   ICandidateResolver 
    {
        private readonly IGravityRuntime _runtime;
        private readonly GravityFieldCandidateFingerprintProvider _provider;
        private readonly CandidateFingerprintBuilder _builder;
        private readonly CandidateResolutionOptions _options;

        public string Name => "Fingerprint";

        public FingerprintCandidateResolver(
            IGravityRuntime runtime,
            GravityFieldCandidateFingerprintProvider provider,
            CandidateFingerprintBuilder builder,
            IOptions<CandidateResolutionOptions> options)
        {
            _runtime = runtime;
            _provider = provider;
            _builder = builder;
            _options = options.Value;
        }

        public CandidateResolutionResult Resolve(
            CandidateResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            CandidateFingerprint incoming =
                _builder.Build(
                    context.IncomingSignature,
                    context.PresenceMask,
                    _options.FingerprintBlockSize);

            List<CandidateFieldRef> candidates = new();

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
                    candidates.Add(
                        new CandidateFieldRef(
                            field.FieldId,
                            i));
                }
            }

            if ( candidates.Count < _options.MinimumCandidateCount &&
                _options.AllowFallbackToAllFields)
            {
                candidates.Clear();

                for ( int i = 0; i < fields.Count; i++)
                {
                    GravityFieldNode field = fields[i];

                    if (!field .IsAllocated )
                    {
                        continue;
                    }

                    candidates.Add(
                        new CandidateFieldRef(
                            field.FieldId, i));
                }

                return new CandidateResolutionResult(
                    candidates,
                    candidates.Count,
                    UsedFallback: true,
                    ResolverName: Name);
            }

            return new CandidateResolutionResult(
                candidates,
                candidates.Count,
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