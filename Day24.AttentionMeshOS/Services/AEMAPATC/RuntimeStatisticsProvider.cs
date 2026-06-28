//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RuntimeStatisticsProvider : IRuntimeStatisticsProvider
    {
        private readonly CrystallizationRuntime _runtime;

        public RuntimeStatisticsProvider(
            CrystallizationRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }
        public RuntimeStatistics GetStatistics()
        {
            var targetSlots = _runtime.Slots;

            int occupiedSlots = 0;
            int totalSlots = 0;

            if( targetSlots is not null )
            {
                totalSlots = targetSlots.Length;

                for ( int i = 0; i < totalSlots; i++ )
                {
                    if (targetSlots[i].IsOccupied)
                    {
                        occupiedSlots++;
                    }
                }
            }

            double slotUtilizationPercentage = 
                totalSlots > 0
                    ? (double)occupiedSlots / totalSlots * 100
                    : 0;

            return new RuntimeStatistics(
                TotalProcessedSignals: 0,
                TotalAcceptedSignals: 0,
                TotalRejectedSignals: 0,
                TotalCrystallizations: _runtime.TotalCrystallizations,
                AverageResonance: 0,
                AverageProcessingDurationMs: 0,
                SlotUtilizationPercentage: slotUtilizationPercentage);
        }
    }
}