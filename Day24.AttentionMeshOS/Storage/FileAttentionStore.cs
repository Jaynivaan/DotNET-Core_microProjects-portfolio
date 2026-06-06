//gs
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.Json;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Storage
{
    public sealed class FileAttentionStore : IAttentionStore
    {
        private readonly ILogger<FileAttentionStore> _logger;
        private readonly string _filePath;
        private readonly List<AttentionBall> _attentionBalls = new();
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public FileAttentionStore(ILogger<FileAttentionStore> logger)
        {
            _logger = logger;

            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "attention-store.json");

            _logger.LogInformation(
                "Attention store file: {Path}",
                _filePath);

            LoadFromFile();
        }

        public void Save(AttentionBall attentionBall)
        {
            _attentionBalls.Add(attentionBall);
            SaveToFile();

        }

        public void Update(AttentionBall attentionBall)
        {
            var index = _attentionBalls.FindIndex(
                ball => ball.Id == attentionBall.Id);
            if (index == -1)
                return;

            _attentionBalls[index] = attentionBall;
            SaveToFile();
        }

        public IReadOnlyList<AttentionBall> GetAll()
        {
            return _attentionBalls;
        }

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                _logger.LogInformation(
                    "Attention Store file doesnot exist yet. starting with Empty store.");
                return;
            }

            _logger.LogInformation(
                "Loading Attention store from file  {path}", _filePath);

            var json = File.ReadAllText(_filePath);

            var balls = JsonSerializer.Deserialize<List<AttentionBall>>(
                json,
                _jsonOptions);

            if (balls == null)
            {
                _logger.LogWarning(
                    "Attention store file {path } was empty or invalid . Starting with new store.",
                    _filePath);
                return;
            }

            _attentionBalls.Clear();
            _attentionBalls.AddRange(balls);

            _logger.LogInformation(
                "Loaded {Count} AttentionBalls from file {Path}.", _attentionBalls.Count, _filePath);
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(
                _attentionBalls,
                _jsonOptions);

            File.WriteAllText(_filePath, json ); 

            _logger.LogInformation(
                "Persisted { Count }  AttentionBalls to file {path}.", 
                _attentionBalls.Count, _filePath);
        }
    }
}