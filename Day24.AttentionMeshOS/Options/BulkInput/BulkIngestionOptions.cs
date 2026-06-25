//gs

namespace Day24.AttentionMeshOS.Options
{
    public sealed class BulkIngestionOptions
    {
        public int MaxInputCharacters { get; set; } = 200_000;

        public int MaxChunkCharacters { get; set; } = 2_000;

        public int ChunkOverlapCharacters { get; set; } = 200;

        public int MaxChunksPerRequest { get; set; } = 150;

        public bool PreserveParagraphs { get; set; } = true;

        public bool ContinueOnChunkFailure { get; set; } = true;

        public bool RejectEmptyChunks { get; set; } = true;


    }
}