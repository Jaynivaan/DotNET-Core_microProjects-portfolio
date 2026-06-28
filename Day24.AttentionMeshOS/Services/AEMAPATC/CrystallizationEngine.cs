//gs
using System;
using Microsoft.Extensions.Logging;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Services;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CrystallizationEngine : ICrystallizationEngine
    {
        private readonly ILogger<CrystallizationEngine> _logger;
        private readonly CrystallizationRuntime _runtime;
        private readonly SlotSelectionEngine _slotSelectionEngine;
        private readonly SignedTernaryResonanceCalculator _resonanceCalculator;
        private readonly AttentionEnergyRouter _energyRouter;
        private readonly CentroidUpdater _centroidUpdater;
        private readonly SignalVocabularyUpdater _vocabularyUpdater;
        private readonly DynamicTagNameBuilder _nameBuilder;
        private readonly DynamicTagBirthFactory _birthFactory;

        public CrystallizationEngine(
            ILogger<CrystallizationEngine> logger,
            CrystallizationRuntime runtime ,
            SlotSelectionEngine slotselectionEngine,
            SignedTernaryResonanceCalculator resonanceCalculator,
            AttentionEnergyRouter energyRouter,
            CentroidUpdater centroidUpdater,
            SignalVocabularyUpdater vocabularyUpdater,
            DynamicTagNameBuilder nameBuilder,
            DynamicTagBirthFactory birthFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
            _slotSelectionEngine = slotselectionEngine ?? throw new ArgumentNullException(nameof(slotselectionEngine));
            _resonanceCalculator = resonanceCalculator ?? throw new ArgumentNullException(nameof(resonanceCalculator));
            _energyRouter = energyRouter ?? throw new ArgumentNullException(nameof(energyRouter));
            _centroidUpdater = centroidUpdater ?? throw new ArgumentNullException(nameof(centroidUpdater));
            _vocabularyUpdater = vocabularyUpdater ?? throw new ArgumentNullException(nameof(vocabularyUpdater));
            _nameBuilder = nameBuilder ?? throw new ArgumentNullException(nameof(nameBuilder));
            _birthFactory = birthFactory ?? throw new ArgumentNullException(nameof(birthFactory));


        }

        public CrystallizationResult Process( CrystallizationContext context )
        {
            if ( !_runtime.Options.Enabled || context is null || context.TernaryMask is null)
            {
                _logger.LogWarning("Crystallization skipped. Enabled= {Enabled}.",_runtime.Options.Enabled);
                return Empty();
            }

            ProtoTagCentroidSlot? slot = ExecuteSelection(context);

            if ( slot is null )
            {
                _logger.LogWarning(
                    "Crystallization skipped. No Available proto-Centroid slot for CorrelationId {CorrelationId}.",
                    context.CorrelationId);
                return Empty();
            }

            bool wasNewSlot = !slot.IsOccupied;

            float resonance = EvolveSlotState(
                slot,
                context,
                wasNewSlot);

            DynamicTagBirth? birth = null;

            if (slot.EnergyState == AttentionEnergyState.Hot)
            {
                birth = FinalizeBirth(slot);

                _runtime.IncrementCrystallizations();

                _logger.LogRegistryBirth(
                   birth.Name,
                   birth.BirthEnergy,
                   birth.BirthStrength,
                   birth.BirthMass);

                _logger.LogInformation(
                    "Dynamic Tag birth completed. CorrelationId = {CorrelationID}, BirthID = {BirthId}, Name ={Name}, Resonance = {Resonance}.",
                    context.CorrelationId,
                    birth.Id,
                    birth.Name,
                    Math.Round(resonance, 4));
            }

            return ComposeResult(
                slot,
                resonance,
                birth);

            
        }

        private ProtoTagCentroidSlot? ExecuteSelection(
            CrystallizationContext context)
        {
            return _slotSelectionEngine.SelectSlot(
                _runtime.Slots,
                context,
                _runtime.Options.ColdThreshold,
                _runtime.Options.WarmThreshold);
        }

        private float EvolveSlotState(
            ProtoTagCentroidSlot slot,
            CrystallizationContext context,
            bool wasNewSlot )
        {
            if (wasNewSlot)
            {
                SeedSlot(
                    slot,
                    context);

                _logger.LogWarning(
                    "New proto-centroid slot needed. CorrelationId = { CorrelationId}, SlotId= {SlotId}.",
                    context.CorrelationId,
                    slot.SlotId);
            }

            float resonance = _resonanceCalculator.Calculate(
                context.TernaryMask,
                slot.TernaryMask);

            slot.LastResonanceScore = resonance;
            slot.AccumulationCount++;

            UpdateAttentionEnergy(
                slot,
                resonance);

            if(_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogEnergyUpdate(
                    slot.SlotId,
                    resonance,
                    slot.AttentionEnergy,
                    slot.SignalStrength,
                    slot.EnergyState.ToString());
            }

            _centroidUpdater.UpdateAccumulator(
                slot,
                context.TernaryMask,
                _runtime.Options.MaxCentroidInertia);

            _centroidUpdater.ProjectMask(slot);

            LogCentroidState(slot);

            _vocabularyUpdater.Update(
                slot,
                context,
                _runtime.Options.MaxSignalPerInput);

            AttentionEnergyState previousState = slot.EnergyState;

            slot.EnergyState = _energyRouter.Resolve(
                slot.AccumulationCount,
                _runtime.Options.WarmPromotionCount,
                _runtime.Options.HotPromotionCount);

            if(_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogLifecycleTransition(
                    slot.SlotId,
                    slot.EnergyState.ToString(),
                    slot.AccumulationCount,
                    slot.AttentionEnergy,
                    slot.SignalStrength,
                    resonance);
            }

            if ( previousState != slot.EnergyState )
            {
                _logger.LogInformation(
                    "Proto-centroid energy state changed. SlotId = {SlotId}, From= {FromState} , To={ToState}, Count = {count}, Resonance = {Resonance}.",
                    slot.SlotId,
                    previousState,
                    slot.EnergyState,
                    slot.AccumulationCount,
                    Math.Round(resonance, 4));
            }

            return resonance;
        }

        private void SeedSlot (
            ProtoTagCentroidSlot slot,
            CrystallizationContext context)
        {
            slot.IsOccupied = true;
            slot.EnergyState = AttentionEnergyState.Cold;
            slot.AccumulationCount = 0;
            slot.AttentionEnergy = 0f;
            slot.SignalStrength = 1.0f;
            slot.LastResonanceScore = 0f;
            slot.LastUpdatedAt = DateTimeOffset.UtcNow;

            _centroidUpdater.UpdateAccumulator(
                slot,
                context.TernaryMask,
                _runtime.Options.MaxCentroidInertia);

            _centroidUpdater.ProjectMask(slot);
        } 

        private void UpdateAttentionEnergy(
            ProtoTagCentroidSlot slot,
            float resonance)
        {
            if (resonance >= _runtime.Options.WarmThreshold)
            {
                slot.AttentionEnergy = Math.Min(
                    slot.AttentionEnergy + 0.02f,
                    5.0f);

                slot.SignalStrength = Math.Min(
                    slot.SignalStrength + 0.05f,
                    2.0f);

                return;
            }

            if (resonance >= _runtime.Options.ColdThreshold)
            {
                slot.AttentionEnergy = Math.Min(
                    slot.AttentionEnergy + 0.10f,
                    5.0f);
            }
        }

        private DynamicTagBirth FinalizeBirth(
            ProtoTagCentroidSlot slot)
        {
            string tagName = _nameBuilder.Build(
                slot.SignalVocabulary);

            DynamicTagBirth birth = _birthFactory.Create(
                tagName,
                slot);
            _runtime.BirthRegistry.Register(birth);
            slot.Reset();
            return birth;
        }

        private static CrystallizationResult ComposeResult(
            ProtoTagCentroidSlot slot,
            float resonance,
            DynamicTagBirth? birth)
        {
            if ( birth is not null )
            {
                return new CrystallizationResult(
                    WasProcessed: true,
                    WasCrystallized: true,
                    CrystallizedTagName: birth.Name,
                    DynamicTagBirthId: birth.Id,
                    SlotIndex: null,
                    EnergyState: AttentionEnergyState.Crystallized,
                    ResonanceScore: resonance);
            }

            return new CrystallizationResult(
                    WasProcessed: true,
                    WasCrystallized: false,
                    CrystallizedTagName: null,
                    DynamicTagBirthId: null,
                    SlotIndex: null,
                    EnergyState: slot.EnergyState,
                    ResonanceScore: resonance);

        }
        private static CrystallizationResult Empty()
        {
            return new CrystallizationResult(
                    WasProcessed: false,
                    WasCrystallized: false,
                    CrystallizedTagName: null,
                    DynamicTagBirthId: null,
                    SlotIndex: null,
                    EnergyState: AttentionEnergyState.Dormant,
                    ResonanceScore: 0f);
        }

        private void LogCentroidState(ProtoTagCentroidSlot slot)
        {
            int positive = 0;
            int neutral = 0;
            int negative = 0;

            for ( int i = 0; i < slot.TernaryMask.Length; i++ )
            {
                sbyte value = slot.TernaryMask[i];

                if( value > 0 )
                {
                    positive++;
                }
                else if (value < 0 )
                {
                   negative++;
                }
                else
                {
                    neutral++;
                }
            }

            if ( _logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogCentroidUpdate(
                    slot.SlotId,
                    slot.AccumulationCount,
                    _runtime.Options.MaxCentroidInertia,
                    positive,
                    neutral,
                    negative );
            }

        }
    }

}
