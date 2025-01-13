using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UseCases;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class DamageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TakeDamage(this IEntity entity, IAtomicValue<float> damage)
        {
            if(!entity.TryGetHealthData(out var health)) return;
            health.Value = DamageCases.CalculateHealthAfterDamage(health, damage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TakeDamage(this IEntity entity, float damage)
        {
            if(!entity.TryGetHealthData(out var health)) return;
            health.Value = DamageCases.CalculateHealthAfterDamage(health, damage);
        }

    }
}