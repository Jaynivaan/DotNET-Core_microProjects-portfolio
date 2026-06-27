//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class DynamicTagNameBuilder
    {
        public string Build(
            IReadOnlyDictionary<string, int>? vocabulary)
        {
            if ( vocabulary is null || vocabulary.Count == 0 )
            {
                return string.Empty;
            }

            string primary = string.Empty;
            string secondary = string.Empty;

            int primaryWeight = int.MinValue;
            int secondaryWeight = int.MinValue;

            foreach ( KeyValuePair<string,int> pair in vocabulary)
            {
                string candidate = pair.Key;
                int weight = pair.Value;

                if ( string.IsNullOrWhiteSpace( candidate ))
                {
                    continue;
                }   
                if ( weight > primaryWeight ||
                    (weight == primaryWeight &&
                    string.CompareOrdinal(candidate, primary) < 0 ))
                {
                    secondary = primary;
                    secondaryWeight = primaryWeight;

                    primary = candidate;
                    primaryWeight = weight;

                    continue;
                }

                if ( string .Equals(
                        candidate,
                        primary,
                        StringComparison.Ordinal))

                {
                    continue;
                }

                if (weight > secondaryWeight ||
                    ( weight == secondaryWeight &&
                    string.CompareOrdinal(candidate, secondary) < 0 ))
                {
                    secondary = candidate;
                    secondaryWeight = weight;
                }
            }

            if ( primary.Length == 0)
            {
                return string.Empty;
            }

            if (secondary.Length == 0 )
            {
                return primary;
            }

            return string.Concat(
                primary,
                "-",
                secondary);
        }
    }
}