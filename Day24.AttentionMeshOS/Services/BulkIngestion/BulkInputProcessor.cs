//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class BulkInputProcessor : IBulkInputProcessor
    {
        private readonly IChunkingService _chunkingService;
        private readonly IAttentionEngine _attentionEngine;
        private readonly BulkIngestionOptions _options;
        private readonly ILogger<BulkInputProcessor> _logger;

        public BulkInputProcessor(
            IChunkingService chunkingService,
            IAttentionEngine attentionEngine,
            IOptions<BulkIngestionOptions> options,
            ILogger<BulkInputProcessor> logger )
        {
            _chunkingService = chunkingService;
            _attentionEngine = attentionEngine;
            _options = options.Value;
            _logger = logger;

        }

        public async Task<BulkInputResponse> ProcessAsync(
            BulkInputRequest request,
            CancellationToken cancellationToken = default)
        {
            if ( request is null )
            {
                throw new ArgumentNullException(nameof(request)); ;
            }
             
            if ( string .IsNullOrWhiteSpace( request.Text ) )
            {
                _logger.LogWarning(
                    "Bulk input request rejected because the input text was empty.");

                return CreateFailedResponse(
                    "Bulk input text cannot be empty.",
                    totalChunks: 0);
            }

            

            if ( request.Text.Length > _options.MaxInputCharacters)
            {
                _logger.LogWarning(
                    "Bulk input rejected because character count {CharacterCount } exceeds configured limit {MaxInputCharacters}.",
                    request.Text.Length,
                    _options.MaxInputCharacters);

                return CreateFailedResponse(
                    $"Bulk input exceeds maximum allowed size of {_options.MaxInputCharacters} characters.",
                    totalChunks: 0);
            }

            var chunks = _chunkingService.Chunk(request.Text);

            if ( chunks.Count == 0 )
            {
                _logger.LogWarning("Bulk input produced no processable chunks.");

                return CreateFailedResponse(
                    "Bulk Input did not produce any processable chunks.",
                    totalChunks: 0);
            }

            if ( chunks.Count > _options.MaxChunksPerRequest)
            {
                _logger.LogWarning(
                    "Bulk input rejected because generated chunk count {ChunkCount} exceeds configured limit {MaxChunksPerRequest}.",
                    chunks.Count,
                    _options.MaxChunksPerRequest);

                return new BulkInputResponse(
                    1,
                    0,
                    1,
                    new List<BulkChunkResult>
                    {
                        CreateChunkResult(
                            0,
                            false,
                            $"Bulk input generated {chunks.Count} chunks, exceeding the maximum allowed {_options.MaxChunksPerRequest}.")
                    });
            }

            _logger.LogInformation(
                "Bulk input processing started with {ChunkCount} chunks.",
                chunks.Count);

            var results = new List<BulkChunkResult>();


            var earlyStopTriggered = false;
            var lastProcessedIndex = 0;

            for ( var index = 0; index < chunks.Count; index++ )
            {

                cancellationToken.ThrowIfCancellationRequested();

                lastProcessedIndex = index;

                var chunkIndex = index + 1;

                var result = await ProcessSingleChunkAsync(
                    chunks[index],
                    chunkIndex,
                    cancellationToken);

                results.Add(result);

                if ( !result .Success && !_options.ContinueOnChunkFailure)
                {
                    _logger.LogWarning(
                        "Bulk input processor stopped after chunk {chunkIndex} failed.",
                        chunkIndex);

                    earlyStopTriggered = true;

                    break;
                }

            }

            var successfulChunks = results.Count(result => result.Success);
            var failedChunks = results.Count(result => !result.Success);

            if ( earlyStopTriggered )
            {
                var skippedCount = chunks.Count - (lastProcessedIndex + 1);
                
                for ( var skippedChunkIndex = lastProcessedIndex + 2;
                        skippedChunkIndex <= chunks.Count;
                        skippedChunkIndex++)
                {
                    results.Add(
                        CreateChunkResult(
                            skippedChunkIndex,
                            false,
                            "Chunk processing skipped because a preceeding chunk failed."));
                }

                failedChunks += skippedCount;
            }

            _logger.LogInformation(
                "Bulk input processing completed. TotalChunks = {TotalChunks}, SuccessfulChunks= {SuccessfulChunks}, FailedChunks ={failedchunks}. ",
                chunks.Count,
                successfulChunks,
                failedChunks);

            return new BulkInputResponse(
                chunks.Count,
                successfulChunks,
                failedChunks,
                results);
        }
        
        private async Task<BulkChunkResult> ProcessSingleChunkAsync(
            string chunk,
            int chunkIndex,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    "Processing bulk chunk {chunkIndex}.",
                    chunkIndex);

                var result = await _attentionEngine.ProcessAsync(
                    chunk,
                    cancellationToken);
                
                if ( result .IsSuccess)
                {
                    return CreateChunkResult(
                        chunkIndex,
                        true);
                }

                var errorMessage =
                    result.InvalidInputResponse?.Message
                    ?? "Chunk Processing Failed. ";

                return CreateChunkResult(
                    chunkIndex,
                    false,
                    errorMessage);
            }

            catch (Exception ex)
            {

                _logger.LogError(
                    ex,
                    "Bulk input chunk {ChunkIndex} failed with an exception.",
                    chunkIndex);

                return CreateChunkResult(
                    chunkIndex,
                    false,
                    ex.Message);
            }
        }

        private static BulkInputResponse CreateFailedResponse(
            string error,
            int totalChunks)
        {
            return new BulkInputResponse(
                totalChunks,
                0,
                1,
                new List<BulkChunkResult>
                {
                    CreateChunkResult(
                        0,
                        false,
                        error)
                });
        }

        private static BulkChunkResult CreateChunkResult(
            int chunkIndex,
            bool success,
            string? error = null)
        {
            return new BulkChunkResult(
                chunkIndex,
                success,
                null,
                error);

        }
    }
}