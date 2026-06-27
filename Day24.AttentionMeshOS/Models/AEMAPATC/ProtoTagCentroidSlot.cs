//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed class ProtoTagCentroidSlot
    {
        public Guid SlotId { get; private set; } = Guid.NewGuid();

        public bool IsOccupied { get; set; }

        public AttentionEnergyState EnergyState { get; set; } = AttentionEnergyState.Dormant;

        public int AccumulationCount { get; set; }

        public float AttentionEnergy { get; set; }

        public float SignalStrength { get; set; } = 1.0f;

        public float LastResonanceScore { get; set; }

        public sbyte[] TernaryMask { get; }

        public int[] CentroidAccumulator { get; }

        public Dictionary<string, int> SignalVocabulary { get; }

        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset LastUpdatedAt { get;  set; } = DateTimeOffset.UtcNow;

        public ProtoTagCentroidSlot(
            int centroidDimensions,
            StringComparer comparer)
        {
            TernaryMask = new sbyte[centroidDimensions];

            CentroidAccumulator = new int [centroidDimensions];

            SignalVocabulary = new Dictionary<string, int>(comparer);
        }

        public void Reset()
        {
            SlotId = Guid.NewGuid();

            IsOccupied = false;

            EnergyState = AttentionEnergyState.Dormant;

            AccumulationCount = 0;

            AttentionEnergy = 0.0f;

            SignalStrength = 1.0f;

            LastResonanceScore = 0.0f;

            Array.Clear(
                TernaryMask,
                0,
                TernaryMask.Length);

            Array.Clear(
                CentroidAccumulator,
                0,
                CentroidAccumulator.Length);

            SignalVocabulary.Clear();

            CreatedAt = DateTimeOffset.UtcNow;

            LastUpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}