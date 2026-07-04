//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFieldCandidateFingerprintProvider
    {
        private readonly CandidateFingerprintBuilder _builder;
        private readonly CandidateResolutionOptions _options;

        public GravityFieldCandidateFingerprintProvider(
            CandidateFingerprintBuilder builder,
            IOptions<CandidateResolutionOptions> options)
        {
            _builder = builder;
            _options = options.Value;
        }

        public CandidateFingerprint Create (
            GravityFieldNode field )
        {
            ArgumentNullException.ThrowIfNull(field);

            return _builder.Build(
                field.FieldSignature,
                field.FieldSignature,
                _options.FingerprintBlockSize);
        }
    }
}