//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;


namespace Day24.AttentionMeshOS.Storage
{
    public sealed class InMemoryRawAttentionInputStore : IRawAttentionInputStore
    {
        private readonly ILogger<InMemoryRawAttentionInputStore> _logger;

        private readonly List<RawAttentionInput> _inputs = new();
        
        public InMemoryRawAttentionInputStore(ILogger<InMemoryRawAttentionInputStore> logger)
        {
            _logger = logger;
        }
        public void Save( RawAttentionInput rawInput)
        {
            _inputs.Add(rawInput);

            _logger.LogInformation(
                "Raw input stored: {InputId}",
                rawInput.Id);
        }

        public void Update (RawAttentionInput rawInput)
        {
            var index = _inputs.FindIndex(
                input => input.Id == rawInput.Id);

            if (index == -1 )
            {
                return;
            }

            _inputs[index] = rawInput;

            _logger.LogInformation(
                "RawAttentionInput Updated: {RawInputId}",
                rawInput.Id);
        }

        public IReadOnlyList<RawAttentionInput>GetAll()
        {
            _logger.LogInformation(
                "Returning {Count} RawAttentionInputs.",
                _inputs.Count);

            return _inputs;
        }
    }
}