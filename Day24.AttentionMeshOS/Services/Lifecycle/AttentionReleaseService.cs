//gs
using Microsoft.Extensions.Logging;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionReleaseService : IAttentionReleaseService
    {
        private readonly IAttentionStore _store;
        private readonly ILogger<AttentionReleaseService> _logger;
        
        public AttentionReleaseService(
            IAttentionStore store,
            ILogger<AttentionReleaseService> logger
            )
        {
            _store = store;
            _logger = logger;
        }

        public DeleteResponse Release(Guid attentionBallId)
        {
            var released = _store.Delete(attentionBallId);

            if (!released)
            {
                _logger.LogWarning(
                    "Attention Ball {id} was not found for release.",
                    attentionBallId);

                return new DeleteResponse(
                    false,
                    "Attention Ball was not found.",
                    0);
            }

            _logger.LogInformation(
                "AttentionBall {Id} released from active mesh.",
                attentionBallId);

            return new DeleteResponse(
                true,
                "AttentionBall  released from active mesh.",
                1);            
        }

        public DeleteResponse ReleaseAll()
        {
            var deletedCount = _store.DeleteAll();

            _logger.LogWarning(
                "All AttentionBalls released from active mesh. DeletedCount: {DeletedCount}.",
                deletedCount);

            return new DeleteResponse(
                true,
                "All AttentionBalls released from active mesh.",
                deletedCount);
        }
    }
}