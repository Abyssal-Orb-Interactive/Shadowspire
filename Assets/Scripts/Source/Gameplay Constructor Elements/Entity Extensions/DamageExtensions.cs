using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class DamageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamageFrom(this IEntity entity, in IEntity damager)
        {
            return damager.TryGetDamageData(out var damage) && entity.TryTakeDamage(damage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamageFrom(this IEntity entity, in GameObject damagerGameObject)
        {
            return damagerGameObject.TryGetComponent(out IEntity damager) && entity.TryTakeDamageFrom(damager);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamageFrom(this IEntity entity, in Component damagerComponent)
        {
            return damagerComponent.TryGetComponent(out IEntity damager) && entity.TryTakeDamageFrom(damager);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamageFrom(this Collider2D entityCollider, in Component damagerComponent)
        {
            return entityCollider.TryGetComponent(out IEntity entity) && entity.TryTakeDamageFrom(damagerComponent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamage(this IEntity entity, in IAtomicValue<float> damage)
        {
           return entity.TryTakeDamage(damage.CurrentValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamage(this IEntity entity, in float damage)
        {
            if(!entity.TryGetHealthData(out var health)) return false;
            health.Value = DamageCases.CalculateHealthAfterDamage(health, damage);
            return true;
        }
    }
}