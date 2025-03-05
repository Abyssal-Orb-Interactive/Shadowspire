using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class InvincibilityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeInvincibility(this IEntity aim)
        {
            if(!aim.TryGetInvincibilityStartEventData(out var invincibilityStartEvent)) return false;

            invincibilityStartEvent.Invoke();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeInvincibility(this AtomicReactiveProperty<IEntity> aim)
        {
            if(!aim.CurrentValue.TryGetInvincibilityStartEventData(out var invincibilityStartEvent)) return false;

            invincibilityStartEvent.Invoke();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeInvincibility(this Collider2D otherEntityCollider)
        {
            return otherEntityCollider.TryGetComponent(out IEntity otherEntity) && TryInvokeInvincibility(otherEntity);
        }
    }
}