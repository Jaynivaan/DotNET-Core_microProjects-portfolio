//gs
using System;
using System.Collections.Generic;
using System.Linq;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateResolverSelector
    {
        private const string AllFieldsResolverName = "AllFields";

        private readonly ILogger<CandidateResolverSelector> _logger;
        private readonly CandidateResolutionOptions _options;
        private readonly IReadOnlyDictionary<string, ICandidateResolver> _resolvers;

        public CandidateResolverSelector (
            ILogger<CandidateResolverSelector> logger,
            IEnumerable<ICandidateResolver> resolvers,
            IOptions<CandidateResolutionOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;

            _resolvers = resolvers.ToDictionary(
                resolver => resolver.Name,
                StringComparer.OrdinalIgnoreCase);
        }
        public ICandidateResolver GetResolver()
        {
            if ( _options.Enabled &&
                _resolvers.TryGetValue(
                    _options.ResolverType,
                    out ICandidateResolver? configuredResolver ))
            {
                AemEsgfTelemetry.CandidateResolverSelected(
                    _logger,
                    configuredResolver.Name);

                return configuredResolver;
            }

            if ( _resolvers.TryGetValue(
                AllFieldsResolverName,
                out ICandidateResolver? fallbackResolver ) )
            {
                AemEsgfTelemetry.CandidateResolverSelected(
                    _logger,
                    fallbackResolver.Name);

                return fallbackResolver;
            }

            throw new InvalidOperationException(
                "Candidate Resolution Configuration Error: All Fields Resolver is not registered.");

        }
    }
}