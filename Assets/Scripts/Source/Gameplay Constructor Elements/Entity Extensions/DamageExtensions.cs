using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
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
            
            if (entity.TryGetCanTakeDamageData(out var canTakeDamage) && !canTakeDamage.CurrentValue) return false;
            
            health.SetUpHealthAfterDamage(damage);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamage(this IEntity entity, in float damage, in DamageType damageType)
        {
            if(!entity.TryGetDamageModifiersData(out var damageModifiers) || !damageModifiers.ContainsKey((int) damageType)) return entity.TryTakeDamage(damage);
            
            var correctedDamage = damage * (1 - damageModifiers[(int) damageType]);
            if(correctedDamage <= 0) correctedDamage = 0;

            correctedDamage = math.round(correctedDamage);
            
            return entity.TryTakeDamage(correctedDamage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryTakeDamage(this IEntity entity, in IAtomicValue<float> damage, in DamageType damageType)
        {
            return entity.TryTakeDamage(damage.CurrentValue, damageType);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetUpHealthAfterDamage(this IAtomicVariable<float> health, in float damage)
        {
            health.Value = DamageCases.CalculateHealthAfterDamage(health, damage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryTakeDamageByPercent(this IEntity entity, in IAtomicValue<float> damagePercent)
        {
            entity.TryGetHealthData(out var health);
            entity.TryGetMaxHealthData(out var maxHealth);
            
            health.Value = DamageCases.CalculateHealthAfterDamageInPercent(health, maxHealth, damagePercent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryTakeDamageByPercent(this IEntity entity, in float damagePercent)
        {
            entity.TryGetHealthData(out var health);
            entity.TryGetMaxHealthData(out var maxHealth);
            
            health.Value = DamageCases.CalculateHealthAfterDamageInPercent(health, maxHealth, damagePercent);
        }
    }
}