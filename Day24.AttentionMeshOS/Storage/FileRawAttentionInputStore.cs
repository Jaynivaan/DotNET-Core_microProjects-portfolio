//gs
using System.Text.Json;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Storage
{
    public sealed class FileRawAttentionInputStore : IRawAttentionInputStore
    {
        private readonly ILogger<FileRawAttentionInputStore> _logger;

        private readonly string _filePath;

        private readonly List<RawAttentionInput> _inputs = new();

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public FileRawAttentionInputStore(ILogger<FileRawAttentionInputStore> logger)
        {
            _logger = logger;

            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "raw-attention-inputs.json");

            _logger.LogInformation(
                "Raw Input store file : {path}",
                _filePath);

            LoadFromFile();
        }

        public void Save(RawAttentionInput rawInput)
        {

            _inputs.Add(rawInput);

            SaveToFile();

            _logger.LogInformation(
                "RawInput persisted: {inputId}",
                rawInput.Id);
        }

        public void Update(RawAttentionInput rawInput)
        {
            var index = _inputs.FindIndex(
                input => input.Id == rawInput.Id);

            if (index == -1)
            {
                return;
            }

            _inputs[index] = rawInput;

            SaveToFile();

            _logger.LogInformation(
                "RawAttentionInput Updated: {RawInputId} and persisted in file.",
                rawInput.Id);
        }

        public IReadOnlyList<RawAttentionInput> GetAll()
        {
            _logger.LogInformation(
                "Returning {Count} RawAttentionInputs.",
                _inputs.Count);

            return _inputs;
        }

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                _logger.LogInformation(
                    "Raw Input file doesnot exist yet. starting with empty input store..");

                    return;                  
            }

            var json = File.ReadAllText(_filePath);

            var inputs = JsonSerializer.Deserialize<List<RawAttentionInput>>
                (json,
                _jsonOptions);

            if (inputs is null)
            {
                _logger.LogWarning(
                    "Raw input file empty or invalid. Starting with empty input store."
                    );
                return;
            }

            _inputs.Clear();
            _inputs.AddRange(inputs);

            _logger.LogInformation(
                "Loaded {Count} Raw Inputs from file.",
                _inputs.Count);
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize
                (
                _inputs,
                _jsonOptions);

            File.WriteAllText(
                _filePath,
                json);

            _logger.LogInformation(
                "Persisted {Count} RawInputs to file.",
                _inputs.Count);
        }
    }
}