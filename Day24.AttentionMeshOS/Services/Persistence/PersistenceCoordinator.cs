//gs
using System;
using Microsoft.Extensions.Options;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class PersistenceCoordinator : IPersistenceCoordinator
    {
        private readonly IDynamicTagPersistenceSerializer _dynamicTags;
        private readonly IGravityRegistryPersistenceSerializer _gravityRegistry;
        private readonly IGravityRuntimePersistenceSerializer _gravityRuntime;
        private readonly IAttentionMeshSaveStore _saveStore;
        private readonly IPersistenceValidator _validator;
        private readonly PersistenceOptions _options;

        //private AttentionMeshSaveFile? _lastSave;

        public PersistenceCoordinator(
            IDynamicTagPersistenceSerializer dynamicTags,
            IGravityRegistryPersistenceSerializer gravityRegistry,
            IGravityRuntimePersistenceSerializer gravityRuntime,
            IAttentionMeshSaveStore saveStore,
            IPersistenceValidator validator,
            IOptions<PersistenceOptions>options)
        {
            _dynamicTags = dynamicTags;
            _gravityRegistry = gravityRegistry;
            _gravityRuntime = gravityRuntime;
            _saveStore = saveStore;
            _validator = validator;
            _options = options.Value;

        }

        public void Save()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            var saveFile = new AttentionMeshSaveFile(
                new SaveMetadata(
                    _options.FormatVersion,
                    "AttentionMeshOS",
                    "1",
                    _options.SignatureLength,
                    _options.SignatureSchemaVersion,
                    _options.QuantizationVersion,
                    now,
                    now,
                    "Guid"),
                _dynamicTags.Capture(),
                _gravityRegistry.Capture(),
                _gravityRuntime.Capture(),
                ReplayJournal: null);

            _saveStore.Save(saveFile);
        }

        public void Restore()
        {
            AttentionMeshSaveFile? saveFile = _saveStore.Load();

            if ( saveFile is null )
            {
                return;
            }
            _validator.Validate( saveFile );


            if ( saveFile.DynamicTags is not null )
            {
                _dynamicTags.Restore(saveFile.DynamicTags);
            }

            if ( saveFile.GravityRegistry is not null )
            {
                _gravityRegistry.Restore( saveFile.GravityRegistry);
            }

            if ( saveFile.GravityRuntime is not null )
            {
                _gravityRuntime.Restore( saveFile.GravityRuntime);
            }
        }
    }
}