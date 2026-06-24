//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RawInputReleaseService : IRawInputReleaseService
    {
        private readonly IRawAttentionInputStore _rawInputStore;
        private readonly IAttentionStore _attentionStore;
        private readonly ILogger<RawInputReleaseService> _logger;

        public RawInputReleaseService(
            IRawAttentionInputStore rawInputStore,
            IAttentionStore attentionStore,
            ILogger<RawInputReleaseService> logger )
        {
            _rawInputStore = rawInputStore;
            _attentionStore = attentionStore;
            _logger = logger;

        }

        public DeleteResponse Release(Guid rawInputId)
        {
            var released = _rawInputStore.Delete( rawInputId );

            if ( !released )
            {
                return new DeleteResponse(
                    false,
                    "Raw input was not found.",
                    0);
            }

            _logger.LogInformation(
                "RawInput {RawInputID} released without Cascade.",
                rawInputId);

            return new DeleteResponse(
                true,
                "Raw input Released. But the associated attentionBalls were preserved.",
                1);
        }

        public DeleteResponse ReleaseAll(bool confirm )
        {
            if ( !confirm )
            {
                return new DeleteResponse(
                    false,
                    "Confirmation Required. Repeat with confirm = true to delete all raw Inputs.",
                    0);
            }

            var deletedCount = _rawInputStore.DeleteAll();

            _logger.LogWarning(
                "All RawInputs released. DeletedCount: {DeletedCount}.",
                deletedCount);

            return new DeleteResponse(
                true,
                "All raw inputs released. Attention Balls were preserved.",
                deletedCount);
        }
        public DeleteResponse CascadeRelease (Guid rawInputId, bool confirm )
        {
            if (!confirm )
            {
                return new DeleteResponse(
                    false,
                    "Cascade Confirmation required.",
                    0);
            }

            var deletedBalls = _attentionStore.DeleteByRawInputId( rawInputId );
            var releasedRawInput = _rawInputStore.Delete(rawInputId);

            if ( !releasedRawInput && deletedBalls == 0)
            {
                return new DeleteResponse(
                    false,
                    "Raw input was not found and no associated attentionBalls were found.",
                    0);

            }

            var totalDeleted = deletedBalls + (releasedRawInput ? 1 : 0);

            _logger.LogWarning(
                "Cascade release completed for RawInput {RawInputId}. DeletedBalls: {DeletedBalls}, RawInputDeleted: {RawInputDeleted}.",
                rawInputId,
                deletedBalls,
                releasedRawInput);

            return new DeleteResponse(
                true,
                "Cascade release completed. Raw input and associated attention lineage were released.",
                totalDeleted);
        }
        
        public DeleteResponse CascadeReleaseAll( bool confirm )
        {
            if ( !confirm )
            {
                return new DeleteResponse(
                    false,
                    "Cascade confirmation required. This will delete all RawInputs and derived attentionBalls, attentionLinks and reinforcementEvents.",
                    0 );

            }

            var deletedBalls = _attentionStore.DeleteAll();
            var deletedRawInputs = _rawInputStore.DeleteAll();

            var totalDeleted = deletedBalls + deletedRawInputs;

            _logger.LogWarning(
                "Full cascade release completed. DeletedRawInputs: {DeletedRawInputs}, DeletedBalls: {deletedBalls}.",
                deletedRawInputs,
                deletedBalls);

            return new DeleteResponse(
                true,
                "Full cascade release completed. All rawinputs and associated lineage artifacts were released.",
                totalDeleted );
        }
    }
}