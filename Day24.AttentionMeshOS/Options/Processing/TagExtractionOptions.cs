//gs
namespace Day24.AttentionMeshOS.Options
{
    public sealed class TagExtractionOptions
    {
        public bool Enabled { get; set; } = true;

        public string RulesFilePath { get; set; } = "Data/tag-rules.json";
    }
}