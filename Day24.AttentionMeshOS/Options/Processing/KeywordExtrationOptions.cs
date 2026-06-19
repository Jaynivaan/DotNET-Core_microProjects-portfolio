//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class KeywordExtractionOptions
    {
        public bool Enabled { get; set; } = true;

        public int MinimumKeywordLength { get; set; } = 3;

        public int MaximumKeywordLength { get; set; } = 10;

        public bool NormalizeKeywords { get; set; } = true;
    }
}