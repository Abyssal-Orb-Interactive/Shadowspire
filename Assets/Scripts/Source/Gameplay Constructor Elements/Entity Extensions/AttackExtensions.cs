using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class AttackExtensions
    {
        public static void UseInMeleeAttack(this IEntity sword)
        {
            if(!sword.TryGetUserData(out var user)) return;
            if(!user.CurrentValue.TryGetMeleeAttackActionEventData(out var meleeAttackActionEvent)) return;
            if(!sword.TryGetDamageData(out var damage)) return;
            if(!sword.TryGetDamageTypeData(out var damageType)) return;
            
            meleeAttackActionEvent.Invoke(damage.CurrentValue, damageType.CurrentValue);
        }
    }
}