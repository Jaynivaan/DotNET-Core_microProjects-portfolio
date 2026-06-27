//gs

using Day24.AttentionMeshOS.Models;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionEnergyRouter
    {
        public AttentionEnergyState Resolve(
            int accumulationCount,
            int warmPromotionCount,
            int hotPromotionCount,
            bool isCrystallizedThisPass = false )
        {
            if ( isCrystallizedThisPass )
            {
                return AttentionEnergyState.Crystallized;
            }

            if ( accumulationCount >= hotPromotionCount )
            {
                return AttentionEnergyState.Hot;
            }
            if ( accumulationCount >= warmPromotionCount )
            {
                return AttentionEnergyState.Warm;
            }
            if ( accumulationCount > 0 )
            {
                return AttentionEnergyState.Cold;
            }

            return AttentionEnergyState.Dormant;
        }
    }
}