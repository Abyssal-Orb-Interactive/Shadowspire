using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;

namespace UseCases
{
    public static class DamageCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in float health, in float damage)
        {
            if(health <= 0) return 0;
            if(damage <= 0) return health;
            
            return health - damage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in IAtomicValue<float> health, in IAtomicValue<float> damage)
        {
            return CalculateHealthAfterDamage(health.CurrentValue, damage.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateHealthAfterDamage(in IAtomicValue<float> health, float damage)
        {
            return CalculateHealthAfterDamage(health.CurrentValue, damage);
        }
    }
}
