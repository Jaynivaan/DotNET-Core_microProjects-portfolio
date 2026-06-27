//gs
using System;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class DynamicTagBirthFactory
    {
        public DynamicTagBirth Create(
            string tagName,
            ProtoTagCentroidSlot slot)
        {
            ArgumentNullException.ThrowIfNull(slot);

            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException(
                    "Tag name cannot be empty.",
                    nameof(tagName));
            }

            sbyte[] signature = new sbyte[slot.TernaryMask.Length];

            Array.Copy(
                slot.TernaryMask,
                signature,
                signature.Length);

            return new DynamicTagBirth(
                Id: Guid.NewGuid(),
                Name: tagName,
                TernarySignature: signature,
                BirthMass: slot.AccumulationCount,
                BirthEnergy: slot.AttentionEnergy,
                BirthStrength: slot.SignalStrength,
                BornAt: DateTimeOffset.UtcNow);
        }
    }
}