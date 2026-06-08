//gs
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
        private readonly List<AttentionLink> _attentionLinks = new();
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

        public void SaveLink(AttentionLink attentionLink)
        {
            _attentionLinks.Add(attentionLink);
            SaveToFile();

            _logger.LogInformation(
                "Persisted AttentionLink from {FromId} to {ToId}.  Strength = {Strength}",
                attentionLink.FromId,
                attentionLink.ToId,
                attentionLink.Strength);

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

        public IReadOnlyList<AttentionLink> GetLinks()
        {
            return _attentionLinks;
        }

        private void LoadFromFile()
        {
            _logger.LogInformation(
                "LoadFromFile called."
                );

            if (!File.Exists(_filePath))
            {
                _logger.LogInformation(
                    "Attention Store file doesnot exist yet. starting with Empty store.");
                return;
            }

            _logger.LogInformation(
                "Loading Attention store from file  {path}", _filePath);

            var json = File.ReadAllText(_filePath);


            var snapshot = JsonSerializer.Deserialize<AttentionStoreSnapshot>(
                json,
                _jsonOptions
                );
                       
            if (snapshot is null) 
            {
                _logger.LogWarning(
                    "Attention store file {path } was empty or invalid . Starting with new store.",
                    _filePath);
                return;
            }

            _attentionBalls.Clear();
            _attentionLinks.Clear();

            _attentionBalls.AddRange(snapshot.AttentionBalls);
            _attentionLinks.AddRange(snapshot.AttentionLinks);
            _logger.LogInformation(
                "Loaded {ballcount} AttentionBalls and {linkCount} AttentionLinks from file {path}.",
                _attentionBalls.Count,
                _attentionLinks.Count,
                _filePath);
        }

        private void SaveToFile()
        {

            var snapshot = new AttentionStoreSnapshot(
                _attentionBalls,
                _attentionLinks
                );

            var json = JsonSerializer.Serialize(
                snapshot,
                _jsonOptions);

            File.WriteAllText(_filePath, json ); 

            _logger.LogInformation(
                "Persisted {Ballcount} AttentionBalls and {linkCount} attentionLinks  to file {path}.",
                _attentionBalls.Count,
                _attentionLinks.Count,
                _filePath);
        }

        public bool Delete(Guid attentionBallId)
        {
            var attentionBall = _attentionBalls
                .FirstOrDefault(ball => ball.Id == attentionBallId);

            if (attentionBall is null)
            {
                _logger.LogWarning(
                    "invalid entry"
                    ); return false;

            }

            _attentionBalls.Remove(attentionBall);

            SaveToFile();

            _logger.LogInformation(
                "Released AttentionBall {id} from file store.", attentionBallId);

            return true;
            
        }
    }
}