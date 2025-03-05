using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using UnityEngine;

namespace UseCases
{
    public static class HealthCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthPercent(in IAtomicValue<float> health, in IAtomicValue<float> maxHealth)
        {
            return CalculateHealthPercent(health.CurrentValue, maxHealth.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthPercent(in IAtomicValue<float> health, in float maxHealth)
        {
            return CalculateHealthPercent(health.CurrentValue, maxHealth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthPercent(in float health, in IAtomicValue<float> maxHealth)
        {
            return CalculateHealthPercent(health, maxHealth.CurrentValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthPercent(in float health, in float maxHealth)
        {
            if(health <= 0f) return 0f;
            if(maxHealth <= 0f) return 0f;
            if(health > maxHealth) return 1f;
            
            return health / maxHealth;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetNewHealthPercent(in IAtomicValue<float> newHealth, in float maxHealth, in IAtomicVariable<float> healthPercent)
        {
            SetNewHealthPercent(newHealth.CurrentValue, maxHealth, healthPercent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetNewHealthPercent(in float newHealth, in IAtomicValue<float> maxHealth, in IAtomicVariable<float> healthPercent)
        {
            SetNewHealthPercent(newHealth, maxHealth.CurrentValue, healthPercent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetNewHealthPercent(in float newHealth, in float maxHealth, in IAtomicVariable<float> healthPercent)
        {
            var tempHealthPercent = CalculateHealthPercent(newHealth, maxHealth);
            
            if(Mathf.Approximately(tempHealthPercent, healthPercent.CurrentValue)) return;
            
            healthPercent.Value = tempHealthPercent;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthValueByPercent(in IAtomicValue<float> maxHealth, in IAtomicValue<float> healthPercent)
        {
            return CalculateHealthValueByPercent(maxHealth.CurrentValue, healthPercent.CurrentValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthValueByPercent(in float maxHealth, in float healthPercent)
        {
            if (maxHealth <= 0f) return 0f;
            
            return healthPercent switch
            {
                <= 0f => 0f,
                >= 1f => maxHealth,
                _ => maxHealth * healthPercent
            };
        }
    }
}