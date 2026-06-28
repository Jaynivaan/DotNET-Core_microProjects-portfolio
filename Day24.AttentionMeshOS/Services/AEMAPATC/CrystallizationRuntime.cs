//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CrystallizationRuntime
    {

        public ProtoTagCentroidSlot[] Slots { get; }

        public CrystallizationOptions Options { get; }

        public IDynamicTagRegistry BirthRegistry { get; }

        public int SlotCount => Slots.Length;

        public CrystallizationRuntime(
            CrystallizationOptions options,
            IDynamicTagRegistry birthRegistry)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(birthRegistry);

            Options = options;
            BirthRegistry = birthRegistry;

            int slotCount = options.SlotCount <= 0 
                ? 512
                : options.SlotCount;

            int centroidDimensions = options.CentroidDimensions <= 0
                ? 64
                : options.CentroidDimensions;

            Slots = new ProtoTagCentroidSlot[slotCount];

            for ( int i = 0; i < Slots.Length; i++ )
            {
                Slots[i] = new ProtoTagCentroidSlot(
                    centroidDimensions,
                    StringComparer.Ordinal);
            }
        }
        private long _totalCrystallizations;
        public long TotalCrystallizations
        {
            get => Interlocked.Read(ref _totalCrystallizations);
            
        }
        public void IncrementCrystallizations()
        {
            Interlocked.Increment(ref _totalCrystallizations);
        }
    }
}