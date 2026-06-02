//gs
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionAnchorService : IAttentionAnchorService
    {
        private readonly ILogger<AttentionAnchorService> _logger;
        
        public AttentionAnchorService
            (
               ILogger<AttentionAnchorService> logger
            )
        {
            _logger = logger;
          
        }

        public bool ShouldCreateAnchor(string userInput)
        {
            var isAnchor =
                userInput.Contains("#anchor", StringComparison.OrdinalIgnoreCase) ||
                userInput.Contains("anchor", StringComparison.OrdinalIgnoreCase);
            if ( isAnchor )
            {
                _logger.LogInformation(
                    "Anchor attention detected from user input ");

            }

            return isAnchor;
        }
    }
}