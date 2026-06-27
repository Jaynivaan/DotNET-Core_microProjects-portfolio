//gs
using System;
using Day24.AttentionMeshOS.Models;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class CentroidUpdater
    {
        public void UpdateAccumulator(
            ProtoTagCentroidSlot? slot,
            sbyte[]? incomingMask,
            int maxCentroidInertia)
        {
            if ( slot is null || incomingMask is null )
            {
                return;
            }

            int length = Math.Min(
                incomingMask.Length,
                slot.CentroidAccumulator.Length);

            for ( int i = 0; i < length; i++ )
            {
                int next = slot.CentroidAccumulator[i] + incomingMask[i];

                if ( next > maxCentroidInertia )
                {
                    next = maxCentroidInertia;
                }
                else if ( next < -maxCentroidInertia )
                {
                    next = -maxCentroidInertia;
                }

                slot.CentroidAccumulator[i] = next;
            }

            slot.LastUpdatedAt = DateTimeOffset.UtcNow;
        }

        public void ProjectMask ( ProtoTagCentroidSlot? slot )
        {
            if ( slot is null )
            {
                return;
            }

            int length = Math.Min(
                slot.CentroidAccumulator.Length,
                slot.TernaryMask.Length);

            for ( int i = 0; i < length; i++ )
            {
                int value = slot.CentroidAccumulator[i];

                slot.TernaryMask[i] = value switch
                {
                    > 0 => (sbyte)1,
                    < 0 => (sbyte)-1,
                    _ => (sbyte)0
                };
            }
        }


    }            
}


