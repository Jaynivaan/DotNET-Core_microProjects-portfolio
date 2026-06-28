//gs
using System;
using System.Diagnostics;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RuntimeHealthProvider : IRuntimeHealthProvider
    {
        private readonly CrystallizationRuntime _runtime;
        private readonly IDynamicTagRegistry _registry;
        private readonly IRuntimeSnapshotProvider _snapshotProvider;
        private readonly Stopwatch _uptimeTracker = Stopwatch.StartNew();

        public RuntimeHealthProvider(
            CrystallizationRuntime runtime,
            IDynamicTagRegistry registry,
            IRuntimeSnapshotProvider snapshotProvider)
        {
            _runtime = runtime;
            _registry = registry;
            _snapshotProvider = snapshotProvider;
        }

        public RuntimeHealth GetHealth()
        {
            bool initialized =
                _runtime.Slots is not null &&
                _runtime.Slots.Length > 0;

            bool registryAvailable = _registry is not null;
            bool snapshotProviderAvailable = _snapshotProvider is not null;

            RuntimeHealthStatus status =
                initialized && registryAvailable && snapshotProviderAvailable
                    ? RuntimeHealthStatus.Healthy
                    : RuntimeHealthStatus.Degraded;

            return new RuntimeHealth(
                Name: AemApatcMetadata.Name,
                Version: AemApatcMetadata.Version,
                Status: status,
                Initialized: initialized,
                RegistryAvailable: registryAvailable,
                SnapshotProviderAvailable: snapshotProviderAvailable,
                Uptime: _uptimeTracker.Elapsed);
        }
    }
}
