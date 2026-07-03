//gs

using System;

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class PersistenceValidationHarness
    {
        private readonly IPersistenceCoordinator _coordinator;
        private readonly IAttentionMeshSaveStore _saveStore;
        private readonly IPersistenceValidator _validator;

        public PersistenceValidationHarness(
            IPersistenceCoordinator coordinator,
            IAttentionMeshSaveStore saveStore,
            IPersistenceValidator validator
            )
        {
            _coordinator = coordinator;
            _saveStore = saveStore;
            _validator = validator;
        }

        public PersistenceValidationResult ValidateSave()
        {
            try
            {
                _coordinator.Save();

                AttentionMeshSaveFile? saveFile =
                    _saveStore.Load();

                if ( saveFile is null )
                {
                    return new PersistenceValidationResult(
                        false,
                        "Save file was not created.");

                }

                _validator.Validate(saveFile);

                return new PersistenceValidationResult(
                    true,
                    "Persistence save validation succeeded.");
            }
            catch(Exception exception)
            {
                return new PersistenceValidationResult(
                    false,
                    exception.Message);

            }
        }
    }
}