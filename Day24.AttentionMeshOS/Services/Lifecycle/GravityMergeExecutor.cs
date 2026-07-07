//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMergeExecutor : IGravityMergeExecutor
    {
        private readonly IGravityRuntime _runtime;
        private readonly IGravityLineageRegistry _lineageRegistry;
    }
}