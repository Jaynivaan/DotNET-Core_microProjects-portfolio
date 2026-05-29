//gs

using Day24.AttentionMeshOS.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class TextSimilarityService : ITextSimilarityService
    {
        public double CalculateSimilarity(
            string firstText,
            string secondText)
        {
            var firstWords = ExtractWords(firstText);
            var secondWords = ExtractWords(secondText);

            if (firstWords.Count == 0 || secondWords.Count == 0)
                return 0;

            var commonWords = firstWords.Intersect(secondWords).Count();
            var totalUniqueWords = firstWords.Union(secondWords).Count();

            return (double)commonWords / totalUniqueWords;


        }

        private static HashSet<string> ExtractWords(
            string text)
        {
            return text
                .ToLowerInvariant()
                .Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                    word.Trim(
                        '.',
                        ',',
                        '!',
                        '?',
                        ';',
                        ':'))
                .Where(word => word.Length > 2)
                .ToHashSet();

        }

    }
}