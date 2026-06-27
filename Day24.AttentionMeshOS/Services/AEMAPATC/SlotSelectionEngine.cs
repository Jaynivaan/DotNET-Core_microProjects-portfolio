//gs
using System;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SlotSelectionEngine
    {
        private readonly SignedTernaryResonanceCalculator _resonanceCalculator;
        private readonly ILogger<SlotSelectionEngine> _logger;
        public SlotSelectionEngine(
            SignedTernaryResonanceCalculator resonanceCalculator,
            ILogger<SlotSelectionEngine> logger
            )
        {
            _resonanceCalculator = resonanceCalculator ??
                throw new ArgumentNullException(nameof(resonanceCalculator));
            _logger = logger;
        }

        public ProtoTagCentroidSlot? SelectSlot(
            ProtoTagCentroidSlot[]? slots,
            CrystallizationContext? context,
            float coldThreshold,
            float warmThreshold)
        {
            if ( slots is null || context is null || context.TernaryMask is null)
            {
                return null;
            }

            ProtoTagCentroidSlot? bestSlot = null;
            ProtoTagCentroidSlot? firstDormantSlot = null;

            float highestScore = 0f;

            for ( int i = 0; i < slots.Length; i++ )
            {
                ProtoTagCentroidSlot slot = slots[i];

                if  ( !slot.IsOccupied )
                {
                    firstDormantSlot ??= slot;
                    continue;
                }

                float resonance =
                    _resonanceCalculator.Calculate(
                        context.TernaryMask,
                        slot.TernaryMask);

                _logger.LogInformation(
                    "Slot {SlotId} | State= {State} | Resonance = {Resonance:F3}.",
                    slot.SlotId,
                    slot.EnergyState,
                    resonance);

                float activeThreshold =
                    slot.EnergyState >= AttentionEnergyState.Warm
                        ? warmThreshold
                        : coldThreshold;

                if ( resonance >= activeThreshold &&
                    resonance > highestScore )
                {
                    highestScore = resonance;
                    bestSlot = slot;

                    _logger.LogInformation(
                        "Slot {SlotId} is current best match ({Score:F3}).",
                        slot.SlotId,
                        resonance);

                }
            }
            ProtoTagCentroidSlot? selected = bestSlot ?? firstDormantSlot;

            _logger.LogInformation(
                "Scheduler Selected Slot={SlotId}, Occupied= {Occupied}",
                selected?.SlotId,
                selected?.IsOccupied);

            return selected;
        }
    }
}