//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class ChunkingService : IChunkingService
    {
        private readonly ILogger<ChunkingService> _logger;
        private readonly BulkIngestionOptions _options;

        public ChunkingService(
            ILogger<ChunkingService> logger,
            IOptions<BulkIngestionOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;
        }
        
        public IReadOnlyList<string> Chunk(string text)
        {
            if ( string .IsNullOrWhiteSpace( text ) )
            {
                _logger.LogWarning(
                    "Bulk chunking skipped because the supplied text was empty.");
                
                return Array.Empty<string>();

            }

            _logger.LogInformation(
                "Starting Bulk chunk generation for { characterCount} characters.",
                text.Length);

            if ( text.Length <= _options.MaxChunkCharacters)
            {
                _logger.LogInformation(
                    "Input fits with in configured chunk size. Generated a single chunk.");

                return new[] { text.Trim() };
            }

            var chunks = new List<string>();

            var start = 0;

            while ( start < text.Length )
            {
                var remaining = text.Length - start;

                var currentMaxLength = Math.Min(
                    _options.MaxChunkCharacters,
                    remaining);
                
                var end = start + currentMaxLength;

                if ( _options.PreserveParagraphs && end < text.Length )
                {
                    var searchStartIndex = end - 1;

                    var paragraphBreak = text.LastIndexOf(
                        "\n\n",
                        searchStartIndex,
                        currentMaxLength,
                        StringComparison.Ordinal);

                    if ( paragraphBreak >=  start )
                    {
                        end = paragraphBreak + 2;
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Paragraph boundary not found at index range {Start} - {End}. Falling Back to Sentence Boundary.",
                            start,
                            end);

                        var sentenceBreak = text.LastIndexOf(
                            ". ",
                            searchStartIndex,
                            currentMaxLength,
                            StringComparison.Ordinal);

                        if ( sentenceBreak >= start )
                        {
                            end = sentenceBreak + 2;
                        }
                        else
                        {
                            _logger.LogWarning(
                                "Sentence boundary not found. Falling back to Character Boundary.");
                        }

                    }
                }

                if ( end <= start )
                {
                    end = start + currentMaxLength;
                }

                var chunk = text[start..end].Trim();

                if ( !string.IsNullOrWhiteSpace( chunk )  ||  !_options.RejectEmptyChunks)
                {
                    chunks.Add( chunk );

                }

                if (end >= text.Length )
                {
                    break;
                }

                var nextStartCandidate = end - _options.ChunkOverlapCharacters;

                start = Math.Max(
                    nextStartCandidate,
                    start + 1);
            }

            _logger.LogInformation(
                "Bulk chunk generation completed. Generated {chunkCount} chunks using MaxChunkCharacters = {MaxChunkCharacters}, ChunkOverlapCharcters = {ChunkOverlapCharacters}.",
                chunks.Count,
                _options.MaxChunkCharacters,
                _options.ChunkOverlapCharacters);

            return chunks;
        }
    }
}

