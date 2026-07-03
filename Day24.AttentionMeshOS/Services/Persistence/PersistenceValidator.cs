//gs
using System;
using System.Collections.Generic;
using System.Linq;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class PersistenceValidator : IPersistenceValidator
    {
        private readonly PersistenceOptions _options;

        public PersistenceValidator(
            IOptions<PersistenceOptions> options )
        {
            _options = options.Value;
        }

        public void Validate( AttentionMeshSaveFile saveFile)
        {
            ArgumentNullException.ThrowIfNull(saveFile);

            ValidateMetadata(saveFile.Metadata);

            if ( saveFile.DynamicTags is null )
            {
                throw new InvalidOperationException(
                    "Persistence validation failed: Dynamic Tags section is missing.");
            }

            if ( saveFile.GravityRegistry is null )
            {
                throw new InvalidOperationException(
                    "Persistence validation failed: GravityRegistry section is missing.");
            }

            if ( saveFile.GravityRuntime is null )
            {
                throw new InvalidOperationException(
                    "Persistence validation failed: GravityRuntime section is missing.");
            }

            ValidateDynamicTags(saveFile.DynamicTags);
            ValidateGravityRegistry(saveFile.GravityRegistry);
            ValidateGravityRuntime(
                saveFile.GravityRuntime,
                saveFile.GravityRegistry,
                saveFile.DynamicTags);
        }

        private void ValidateMetadata(
            SaveMetadata metadata )
        {
            if ( metadata.FormatVersion !=  _options.FormatVersion )
            {
                throw new InvalidOperationException(
                    "Persistence Validation Failed:  unsupported Format Version.");
            }

            if ( metadata.SignatureLength != _options.SignatureLength )
            {
                throw new InvalidOperationException(
                    "Persistence Validation Failed: signatureLength mismatch.");
            }

            if ( metadata.SignatureSchemaVersion != _options.SignatureSchemaVersion )
            {
                throw new InvalidOperationException(
                    "Persistence Validation Failed: signature schema version mismatch.");
            }

            if (metadata.QuantizationVersion != _options.QuantizationVersion )
            {
                throw new InvalidOperationException(
                    "Persistence Validation Failed: quantization Version Mismatch.");
            }
        }

        private static void ValidateDynamicTags(
            DynamicTagRegistryState state)
        {
            HashSet<Guid> ids = new();

            foreach ( DynamicTagBirthState tag in state.Tags )
            {
                if (!ids.Add(tag.Id ))
                {
                    throw new InvalidOperationException(
                        "Persistence Validation Failed: duplicate Dynamic Tag Id.");
                }
            }
        }

        private static void ValidateGravityRegistry(
            GravityRegistryState state )
        {
            HashSet<Guid> ids = new();

            foreach (GravityFieldIdentityState field in state.Fields )
            {
                if ( !ids.Add(field.FieldId ))
                {
                    throw new InvalidOperationException(
                        "Persistence Validation Failed: duplicate Gravity Field Id.");
                }
            }
        }

        private static void ValidateGravityRuntime(
            GravityRuntimeState runtime,
            GravityRegistryState registry,
            DynamicTagRegistryState dynamicTags
            )
        {
            HashSet<Guid> registryFieldIds = registry
                .Fields
                .Select(field => field.FieldId)
                .ToHashSet();
            HashSet<Guid> dynamicTagIds = dynamicTags
                .Tags
                .Select(tag => tag.Id)
                .ToHashSet();

            foreach (GravityFieldRuntimeState field in runtime.Fields)
            {
                if (!registryFieldIds.Contains(field.FieldId))
                {
                    throw new InvalidOperationException(
                        "Persistence Validation Failed: runtime field missing registry identity.");
                }

                if ( field.Physics is  null)
                {
                    throw new InvalidOperationException(
                        "Persistence Validation Failed: runtime field missing Physics state.");
                }

                HashSet<Guid> participantIds = new();

                foreach(DynamicTagParticipationState participant in field.Participants)
                {
                    if (!participantIds.Add(participant.DynamicTagId))
                    {
                        throw new InvalidOperationException(
                            "Persistence Validation Failed: duplicate participant identity within field.");
                    }

                    if ( !dynamicTagIds.Contains(participant.DynamicTagId))
                    {
                        throw new InvalidOperationException(
                            "Persistence Validation Failed: participant references missing DynamicTag.");
                    }
                }
            }
        }
    }
}