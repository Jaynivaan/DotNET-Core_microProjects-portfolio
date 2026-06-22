//gs
using System.Text.Json;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Storage
{
    public sealed class FileAttentionBallMetadataStore : IAttentionBallMetadataStore
    {
        private readonly ILogger<FileAttentionBallMetadataStore> _logger;
        private readonly string _filePath;
        private readonly object _lock = new();

        private readonly Dictionary<Guid, AttentionBallMetadata> _metadataByBallId;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public FileAttentionBallMetadataStore(
            ILogger<FileAttentionBallMetadataStore> logger)
        {
            _logger = logger;

            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "attention-ball-metadata.json");

            _metadataByBallId = LoadFromFile();

            _logger.LogInformation(
                "AttentionBall metadata store file : {filePath}",
                _filePath);
        }

        public void Save(AttentionBallMetadata metadata)
        {
            lock ( _lock )
            {
                _metadataByBallId[metadata.AttentionBallId] = metadata;
                SaveToFile();
            }
        }

        public IReadOnlyList<AttentionBallMetadata> GetAll()
        {
            lock (_lock )
            {
                return _metadataByBallId.Values.ToList();
            }
        }

        public AttentionBallMetadata? GetByAttentionBallId(
            Guid attentionBallId)
        {
            lock( _lock )
            {
                return _metadataByBallId.TryGetValue(
                    attentionBallId,
                    out var metadata )
                    ? metadata : null;

            }
        }

        public bool Exists(Guid attentionBallId)
        {
            lock (_lock )
            {
                return _metadataByBallId.ContainsKey(
                    attentionBallId);
            }
        }

        private Dictionary<Guid, AttentionBallMetadata> LoadFromFile()
        {
            if (!File.Exists( _filePath ))
            {
                return new Dictionary<Guid, AttentionBallMetadata>();
            }

            var json = File.ReadAllText( _filePath );

            if (string.IsNullOrWhiteSpace(json))
            {
                return new Dictionary<Guid, AttentionBallMetadata>();
            }

            return JsonSerializer.Deserialize<
                        Dictionary<Guid, AttentionBallMetadata>>(
                        json,
                        JsonOptions)
                    ?? new Dictionary<Guid, AttentionBallMetadata>();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(
                _metadataByBallId,
                JsonOptions
                );

            File.WriteAllText( _filePath, json );
        }
    }
}