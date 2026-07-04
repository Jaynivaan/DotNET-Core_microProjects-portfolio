//gs
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AllFieldsCandidateResolver : ICandidateResolver
    {
        private readonly IGravityRuntime _runtime;

        public string Name => "AllFields";

        public AllFieldsCandidateResolver (
            IGravityRuntime runtime)
        {
            _runtime = runtime;
        }

        public CandidateResolutionResult Resolve(
            CandidateResolutionContext context)
        {
            List<CandidateFieldRef> candidates = new();

            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;

            for ( int i = 0; i < fields.Count; i++ )
            {
                GravityFieldNode field = fields[i];

                if ( !field .IsAllocated)
                {
                    continue;
                }
                candidates.Add(
                    new CandidateFieldRef(
                        field.FieldId,
                        i));
            }

            return new CandidateResolutionResult(
                candidates,
                candidates.Count,
                UsedFallback: false,
                ResolverName: nameof(AllFieldsCandidateResolver)
                );
        }
    }
}