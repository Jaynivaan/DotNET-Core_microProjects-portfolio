using System.ComponentModel.DataAnnotations;

namespace Day24.AttentionMeshOS.Options
{
    public sealed class PeristenceOptions
    {
        public bool Enabled { get; set; } = true;

        [Required]
        public string DataDirectory { get; set; } = "Peristence";

        [Required]
        public string SaveFileName { get; set; } = "amos.json";

        [Range(1, int.MaxValue)]
        public int FormatVersion { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int SignatureLength { get; set; } = 64;

        [Range(1, int.MaxValue)]
        public int SignatureSchemaVersion { get; set; } = 1;

        [Range(1, int.MaxValue)] 
        public int QuantizationVersion { get; set; } = 1;

    }
}