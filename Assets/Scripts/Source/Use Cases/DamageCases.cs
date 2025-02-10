using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;

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
    }
}
