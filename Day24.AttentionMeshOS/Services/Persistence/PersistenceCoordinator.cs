//gs
using System;
using Microsoft.Extensions.Options;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<PersistenceCoordinator> _logger;

        //private AttentionMeshSaveFile? _lastSave;

        public PersistenceCoordinator(
            IDynamicTagPersistenceSerializer dynamicTags,
            IGravityRegistryPersistenceSerializer gravityRegistry,
            IGravityRuntimePersistenceSerializer gravityRuntime,
            IAttentionMeshSaveStore saveStore,
            IPersistenceValidator validator,
            IOptions<PersistenceOptions>options,
            ILogger<PersistenceCoordinator> logger)
        {
            _dynamicTags = dynamicTags;
            _gravityRegistry = gravityRegistry;
            _gravityRuntime = gravityRuntime;
            _saveStore = saveStore;
            _validator = validator;
            _options = options.Value;
            _logger = logger;

        }

        public void Save()
        {
            try
            {
                AemEsgfTelemetry.PersistenceSaveStarted(
                    _logger,
                    _options.FormatVersion);

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
                    ReplayJournal: null,
                    GravityLineage: null
                    );

                _saveStore.Save(saveFile);

                AemEsgfTelemetry.PersistenceSaveCompleted(
                    _logger,
                    _options.FormatVersion);
            }
            catch ( Exception exception )
            {
                AemEsgfTelemetry.PersistenceSaveFailed(
                    _logger,
                    exception);

                throw;
            }            
        }

        public void Restore()
        {

            try
            {
                AemEsgfTelemetry.PersistenceLoadStarted(
                    _logger );

                AttentionMeshSaveFile? saveFile = _saveStore.Load();

                AemEsgfTelemetry.PersistenceLoadCompleted(
                    _logger,
                    saveFile is not null);

                if (saveFile is null)
                {
                    return;
                }

                AemEsgfTelemetry.PersistenceValidationStarted(
                    _logger,
                    saveFile.Metadata.FormatVersion);

                try
                {
                    _validator.Validate(saveFile);
                }
                catch (Exception exception)
                {
                    AemEsgfTelemetry.PersistenceValidationFailed(
                    _logger,
                    exception);

                    throw;
                }

                AemEsgfTelemetry.PersistenceRestoreStarted(
                    _logger );

                if (saveFile.DynamicTags is not null)
                {
                    _dynamicTags.Restore(saveFile.DynamicTags);
                }

                if (saveFile.GravityRegistry is not null)
                {
                    _gravityRegistry.Restore(saveFile.GravityRegistry);
                }

                if (saveFile.GravityRuntime is not null)
                {
                    _gravityRuntime.Restore(saveFile.GravityRuntime);
                }

                AemEsgfTelemetry.PersistenceRestoreCompleted(
                    _logger );
            }
            catch ( Exception exception )
            {
                AemEsgfTelemetry.PersistenceRestoreFailed(
                    _logger,
                    exception);
                
                throw;
            }            
        }
    }
}