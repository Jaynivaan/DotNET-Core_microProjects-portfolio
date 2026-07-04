//gs
using System;
using System.Collections.Generic;
using System.Linq;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateResolverSelector
    {
        private const string AllFieldsResolverName = "AllFields";

        private readonly CandidateResolutionOptions _options;
        private readonly IReadOnlyDictionary<string, ICandidateResolver> _resolvers;

        public CandidateResolverSelector (
            IEnumerable<ICandidateResolver> resolvers,
            IOptions<CandidateResolutionOptions> options
            )
        {
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
                return configuredResolver;
            }

            if ( _resolvers.TryGetValue(
                AllFieldsResolverName,
                out ICandidateResolver? fallbackResolver ) )
            {
                return fallbackResolver;
            }

            throw new InvalidOperationException(
                "Candidate Resolution Configuration Error: All Fields Resolver is not registered.");

        }
    }
}