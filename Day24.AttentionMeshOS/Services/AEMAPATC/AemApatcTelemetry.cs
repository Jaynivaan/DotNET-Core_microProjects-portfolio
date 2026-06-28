//gs
using System;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public static partial class AemApatcTelemetry
    {
        [LoggerMessage(
            EventId = 1001,
            Level = LogLevel.Information,
            Message = "AEM-APATC lifeCycle: Slot = {SlotId}, State= {State}, Count={Count}, Energy={Energy}, Strength={Strength}, Resonance={Resonance}")]
        public static partial void LogLifecycleTransition(
            this ILogger logger,
            Guid slotId,
            string state,
            int count,
            float energy,
            float strength,
            float resonance);

        [LoggerMessage(
            EventId = 1101,
            Level = LogLevel.Information,
            Message = "AEM-APATC centroid: Slot={SlotId}, Count={Count}, Inertia={Inertia}, Positive={Positive}, Neutral= {Neutral}, Negative= {Negative}")]
        public static partial void LogCentroidUpdate(
            this ILogger logger,
            Guid slotId,
            int count,
            int inertia,
            int positive,
            int neutral,
            int negative);

        [LoggerMessage(
            EventId = 1201,
            Level = LogLevel.Information,
            Message = "AEM-APATC energy: Slot={SlotId}, Resonance={Resonance}, Energy={Energy}, Strength={Strength}, State= {State}")]
        public static partial void LogEnergyUpdate(
            this ILogger logger,
            Guid slotId,
            float resonance,
            float energy,
            float strength,
            string state
            );

        [LoggerMessage(
            EventId = 1301,
            Level = LogLevel.Information,
            Message = "AEM-APATC registry: Name={Name} , BirthEnergy={BirthEnergy}, BirthStrength={BirthStrength}, BirthMass={BirthMass}")]
        public static partial void LogRegistryBirth(
            this ILogger logger,
            string name,
            float birthEnergy,
            float birthStrength,
            float birthMass
            );

        [LoggerMessage(
           EventId = 1401,
           Level = LogLevel.Information,
           Message = "AEM-APATC pipeline: Executing={Processor}")]
        public static partial void LogPipelineProcessor(
           this ILogger logger,
           string processor);


        [LoggerMessage(
          EventId = 1501,
          Level = LogLevel.Information,
          Message = "AEM-APATC snapshot: TotalSlots={TotalSlots}, Occupied= {OccupiedSlots}, Dormant={DormantSlots}, RegistrySize={RegistrySize}, AvgResonance={AverageResonance}, HighestEnergy={HighestEnergy}")]
        public static partial void LogRuntimeSnapshot(
          this ILogger logger,
          int totalSlots,
          int occupiedSlots,
          int dormantSlots,
          int registrySize,
          double averageResonance,
          double highestEnergy);




    }
}