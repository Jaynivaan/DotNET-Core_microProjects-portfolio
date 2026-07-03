//gs
using System;
using System.IO;
using System.Text.Json;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class JsonAttentionMeshSaveStore : IAttentionMeshSaveStore
    {
        private readonly PersistenceOptions _options;

        private readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public JsonAttentionMeshSaveStore(
            IOptions<PersistenceOptions> options
            )
        {
            _options = options.Value;
        }

        public AttentionMeshSaveFile? Load()
        {
            string path = GetSavePath();

            if ( !File.Exists( path ) )
            {
                return null;
            }

            FileInfo fileInfo = new(path);

            if ( fileInfo.Length == 0 )
            {
                return null;
            }

            string json = File.ReadAllText(path);

            if ( string .IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<AttentionMeshSaveFile>(
                    json,
                    JsonOptions);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public void Save(
            AttentionMeshSaveFile saveFile  )

        {
            ArgumentNullException.ThrowIfNull(saveFile);

            Directory.CreateDirectory(_options.DataDirectory);

            string path = GetSavePath();
            string tempPath = path + ".tmp";

            string json = JsonSerializer.Serialize(
                saveFile,
                JsonOptions);

            File.WriteAllText(tempPath, json);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.Move(tempPath, path);
        }

        private string GetSavePath()
        {
            return Path.Combine(
                _options.DataDirectory,
                _options.SaveFileName);
        }

    }
}