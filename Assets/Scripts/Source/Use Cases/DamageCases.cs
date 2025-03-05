using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using UnityEngine;

namespace UseCases
{
    public static class DamageCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in float health, in float damage)
        {
            if(health <= 0f) return 0f;
            if(damage <= 0f) return health;
            if (damage >= health) return 0f;
            
            return health - damage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in IAtomicValue<float> health, in IAtomicValue<float> damage)
        {
            return CalculateHealthAfterDamage(health.CurrentValue, damage.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in IAtomicValue<float> health, in float damage)
        {
            return CalculateHealthAfterDamage(health.CurrentValue, damage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateDamageByPercent(in IAtomicValue<float> maxHealth, in IAtomicValue<float> damageInPercent)
        {
            return CalculateDamageByPercent(maxHealth.CurrentValue, damageInPercent.CurrentValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateDamageByPercent(in float maxHealth, in float damageInPercent)
        {
            if (maxHealth <= 0f) return 0f;
            if (damageInPercent <= 0f) return 0f;
            
            return maxHealth * damageInPercent;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamageInPercent(in IAtomicValue<float> health, in IAtomicValue<float> maxHealth, in IAtomicValue<float> damageInPercent)
        {
            return CalculateHealthAfterDamageInPercent(health.CurrentValue, maxHealth.CurrentValue, damageInPercent.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamageInPercent(in float health, in float maxHealth, in float damageInPercent)
        {
            return health - CalculateDamageByPercent(maxHealth, damageInPercent);
        }

        public static float CalculateHealthAfterDamageInPercent(AtomicReactiveProperty<float> health, AtomicReactiveProperty<float> maxHealth, in float damageInPercent)
        {
            return CalculateHealthAfterDamageInPercent(health.CurrentValue, maxHealth.CurrentValue, damageInPercent);
        }
    }
}
