//gs
using System;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RuntimeSnapshotProvider : IRuntimeSnapshotProvider
    {
        private readonly ILogger<RuntimeSnapshotProvider> _logger;
        private readonly CrystallizationRuntime _runtime;
        private readonly IDynamicTagRegistry _registry;
        private readonly Stopwatch _uptimeTracker = Stopwatch.StartNew();

        public RuntimeSnapshotProvider(
            ILogger<RuntimeSnapshotProvider> logger,
            CrystallizationRuntime runtime,
            IDynamicTagRegistry registry
            )
        {
            _logger = logger;
            _runtime = runtime;
            _registry = registry;
        }

        public RuntimeSnapshot GetSnapshot()
        {
            var localSlots = _runtime.Slots;

            int totalSlots = localSlots.Length;
            int occupied = 0;
            int dormant = 0;
            int cold = 0;
            int warm = 0;
            int hot = 0;

            double totalResonance = 0;
            double highestEnergy = 0;

            for ( int i = 0; i < totalSlots; i++ )
            {
                ProtoTagCentroidSlot slot = localSlots[i];

                if ( slot.IsOccupied )
                {
                    occupied++;
                }

                switch ( slot.EnergyState )
                {
                    case AttentionEnergyState.Dormant: dormant++; break;

                    case AttentionEnergyState.Cold: cold++; break;

                    case AttentionEnergyState.Warm: warm++; break;

                    case AttentionEnergyState.Hot: hot++; break;
                }

                totalResonance += slot.LastResonanceScore;
                
                if ( slot.AttentionEnergy > highestEnergy)
                {
                    highestEnergy = slot.AttentionEnergy;
                }               
            }
            int registryCount = _registry.GetCount();

            double averageResonance =
                occupied > 0
                ? totalResonance / occupied
                : 0;

            var snapshot = new RuntimeSnapshot(
                Uptime: _uptimeTracker.Elapsed,
                TotalSlots: totalSlots,
                OccupiedSlots: occupied,
                DormantSlots: dormant,
                ColdSlots: cold,
                WarmSlots: warm,
                HotSlots: hot,
                RegistrySize: registryCount,
                CrystallizationCount: checked((int)_runtime.TotalCrystallizations),
                AverageResonance: averageResonance,
                HighestEnergy: highestEnergy,
                AverageProcessingLatencyMs: 0);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogRuntimeSnapshot(
                    snapshot.TotalSlots,
                    snapshot.OccupiedSlots,
                    snapshot.DormantSlots,
                    snapshot.RegistrySize,
                    snapshot.AverageResonance,
                    snapshot.HighestEnergy);
            }

            return snapshot;
        }
    }
}