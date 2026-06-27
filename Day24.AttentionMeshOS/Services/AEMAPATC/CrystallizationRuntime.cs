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

        public IDynamicTagBirthStore BirthStore { get; }

        public int SlotCount => Slots.Length;

        public CrystallizationRuntime(
            CrystallizationOptions options,
            IDynamicTagBirthStore birthStore)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(birthStore);

            Options = options;
            BirthStore = birthStore;

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
    }
}