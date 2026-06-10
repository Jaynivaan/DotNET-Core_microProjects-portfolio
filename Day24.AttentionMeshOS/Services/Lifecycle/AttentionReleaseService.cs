//gs
using Microsoft.Extensions.Logging;
using Day24.AttentionMeshOS.Abstractions;
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

        public bool Release(Guid attentionBallId)
        {
            var released = _store.Delete(attentionBallId);

            if (released)
            {
                _logger.LogInformation(
                    "Attention Ball {id} released from active mesh.",
                    attentionBallId
                    );
            }
            return released;
        }
    }
}