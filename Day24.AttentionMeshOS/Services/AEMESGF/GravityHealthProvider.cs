//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityHealthProvider : IGravityHealthProvider
    {
        private readonly IGravityRuntime _runtime;
        private readonly IGravityRegistry _registry;
        private readonly IGravitySnapshotProvider _snapshotProvider;
        private readonly IGravityStatisticsProvider _statisticsProvider;
        private readonly IGravityFormationEngine _formationEngine;


        public GravityHealthProvider (
            IGravityRuntime runtime,
            IGravityRegistry registry,
            IGravitySnapshotProvider snapshotProvider,
            IGravityStatisticsProvider statisticsProvider,
            IGravityFormationEngine formationEngine
            )
        {
            _runtime = runtime;
            _registry = registry;
            _snapshotProvider = snapshotProvider;
            _statisticsProvider = statisticsProvider;
            _formationEngine = formationEngine;
        }

        public GravityRuntimeHealth GetHealth()
        {
            bool initialized = 
                _runtime.Fields is not null && 
                _runtime.FieldCount > 0;

            bool registryAvailable = _registry is not null;
            bool snapshotProviderAvailable = _snapshotProvider is not null;
            bool statisticalProviderAvailable = _statisticsProvider is not null;
            bool formationEngineAvailable = _formationEngine is not null;

            GravityRuntimeHealthStatus status = 
                initialized &&
                registryAvailable &&
                snapshotProviderAvailable &&
                statisticalProviderAvailable &&
                formationEngineAvailable
                    ? GravityRuntimeHealthStatus.Healthy
                    : GravityRuntimeHealthStatus.Degraded;

            return new GravityRuntimeHealth(
                Name: "AEM-ESGF",
                Version: "1.0.0",
                Status: status,
                RuntimeInitialized: initialized,
                RegistryAvailable: registryAvailable,
                SnapshotProviderAvailable: snapshotProviderAvailable,
                StatisticsProviderAvailable: statisticalProviderAvailable,
                FormationEngineAvailable: formationEngineAvailable,
                Uptime: _runtime.Uptime );
        }
    }
}