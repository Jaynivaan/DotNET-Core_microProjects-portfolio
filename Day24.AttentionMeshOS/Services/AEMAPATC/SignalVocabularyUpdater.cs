//gs
using System;
using System.Buffers;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SignalVocabularyUpdater
    {
        public void Update(
            ProtoTagCentroidSlot? slot,
            CrystallizationContext? context,
            int maxSignalsPerInput)
        {
            if (slot is null || context is null || maxSignalsPerInput <= 0)
            {
                return;
            }

            string[] seenSignals = ArrayPool<string>.Shared.Rent(maxSignalsPerInput);

            int seenCount = 0;

            try
            {
                seenCount = MergeSignals(
                    slot,
                    context.Keywords,
                    seenSignals,
                    seenCount,
                    maxSignalsPerInput);

                seenCount = MergeSignals(
                    slot,
                    context.ExtractedTags,
                    seenSignals,
                    seenCount,
                    maxSignalsPerInput);

                if ( seenCount == 0 &&
                        !string.IsNullOrWhiteSpace(context.SourceText))                    
                {
                    string fallbackSignal = context.SourceText.Trim();

                    if (slot.SignalVocabulary.TryGetValue(fallbackSignal, out int count))
                    {
                        slot.SignalVocabulary[fallbackSignal] = count + 1;
                    }
                    else
                    {
                        slot.SignalVocabulary.Add(fallbackSignal, 1);
                    }

                }
            }
            finally
            {
                Array.Clear(
                    seenSignals,
                    0,
                    seenCount);
                ArrayPool<string>.Shared.Return(seenSignals);
            }
        }

        private static int MergeSignals(
            ProtoTagCentroidSlot slot,
            IReadOnlyCollection<string>? signals,
            string[] seenSignals,
            int seenCount,
            int maxSignals)
        {
            if ( signals is null )
            {
                return seenCount;
            }

            foreach (string signal in signals)
            {
                if (seenCount >= maxSignals)
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(signal))
                {
                    continue;
                }

                if (HasSeenSignal(
                        seenSignals,
                        seenCount,
                        signal))
                {
                    continue;

                }

                seenSignals[seenCount++] = signal;

                if (slot.SignalVocabulary.TryGetValue(
                        signal,
                        out int count))
                {
                    slot.SignalVocabulary[signal] = count + 1;
                }
                else
                {
                    slot.SignalVocabulary.Add(signal, 1);
                }
            }
            return seenCount;
        }
        private static bool HasSeenSignal(
            string[] seenSignals,
            int seenCount,
            string signal)
        {
            for (int i = 0; i < seenCount; i++)
            {
                if (string .Equals(
                    seenSignals[i],
                    signal,
                    StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}