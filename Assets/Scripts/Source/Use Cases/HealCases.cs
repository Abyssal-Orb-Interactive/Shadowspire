using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;

namespace UseCases
{
    public static class HealCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterPercentHeal(in IAtomicValue<float> maxHealth, in IAtomicValue<float> currentHealth, in IAtomicValue<float> healPercent)
        {
            return CalculateHealthAfterPercentHeal(maxHealth.CurrentValue, currentHealth.CurrentValue, healPercent.CurrentValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterPercentHeal(in float maxHealth, in float currentHealth, in float healPercent)
        {
            if (maxHealth <= 0f) return 0f;
            if (currentHealth <= 0f) return 0f;
            if (currentHealth >= maxHealth)  return maxHealth;
            
            switch (healPercent)
            {
                case <= 0f:
                    return 0f;
                case >= 1f:
                    return maxHealth;
            }

            var healAmount = HealthCases.CalculateHealthValueByPercent(maxHealth, healPercent);
            if(currentHealth + healAmount >= maxHealth) return maxHealth;
            return currentHealth + healAmount;
        }
    }
}