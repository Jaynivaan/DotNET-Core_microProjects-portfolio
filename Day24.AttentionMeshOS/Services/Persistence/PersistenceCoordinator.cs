//gs
using System;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class PersistenceCoordinator : IPersistenceCoordinator
    {
        private readonly IDynamicTagPersistenceSerializer _dynamicTags;
        private readonly IGravityRegistryPersistenceSerializer _gravityRegistry;
        private readonly IGravityRuntimePersistenceSerializer _gravityRuntime;

        private AttentionMeshSaveFile? _lastSave;

        public PersistenceCoordinator(
            IDynamicTagPersistenceSerializer dynamicTags,
            IGravityRegistryPersistenceSerializer gravityRegistry,
            IGravityRuntimePersistenceSerializer gravityRuntime)
        {
            _dynamicTags = dynamicTags;
            _gravityRegistry = gravityRegistry;
            _gravityRuntime = gravityRuntime;

        }

        public void Save()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            _lastSave = new AttentionMeshSaveFile(
                new SaveMetadata(
                    FormatVersion: 1,
                    RuntimeVersion: "AttentionMeshOS",
                    PersistenceVersion: "1",
                    SignatureLength: 64,
                    SignatureSchemaVersion: 1,
                    QuantizationVersion: 1,
                    CreatedAt: now,
                    SavedAt: now,
                    SemanticIdentityMode: "Guid"),
                _dynamicTags.Capture(),
                _gravityRegistry.Capture(),
                _gravityRuntime.Capture(),
                ReplayJournal: null);
        }

        public void Restore()
        {
            if ( _lastSave is null )
            {
                return;
            }

            if ( _lastSave.DynamicTags is not null )
            {
                _dynamicTags.Restore(_lastSave.DynamicTags);
            }

            if ( _lastSave.GravityRegistry is not null )
            {
                _gravityRegistry.Restore( _lastSave.GravityRegistry);
            }

            if ( _lastSave.GravityRuntime is not null )
            {
                _gravityRuntime.Restore( _lastSave.GravityRuntime);
            }
        }
    }
}