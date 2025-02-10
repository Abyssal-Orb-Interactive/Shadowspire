using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class AttackExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryUseInMeleeAttack(this IEntity attacker, in IEntity weapon)
        {
            if(!attacker.TryGetMeleeAttackActionEventData(out var meleeAttackActionEvent)) return false;
            if(!weapon.TryGetDamageData(out var damage)) return false;
            if(!weapon.TryGetDamageTypeData(out var damageType)) return false;
            
            meleeAttackActionEvent.Invoke(damage.CurrentValue, damageType.CurrentValue);
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryUseInMeleeAttack(this IAtomicValue<IEntity> attacker, in IEntity weapon)
        {
            return attacker.CurrentValue.TryUseInMeleeAttack(weapon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySplashAttackAllTargetsInDamageZoneWith(this IEntity entity, in float damage, in DamageType damageType)
        {
            if(!entity.TryGetTargetsInDamageZoneData(out var targetsInDamageZone)) return false;
                
            var count = targetsInDamageZone.Count;

            for (var i = 0; i < count; i++)
            {
                targetsInDamageZone[i].TryTakeDamage(damage, damageType);
            }
            
            return true;
        }
    }
}