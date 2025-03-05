using System.Runtime.CompilerServices;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class TouchInteractionEventExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeTouchInteractionEventWith(this Component component, in Collider2D other)
        {
            return component.TryGetComponent(out IEntity entity) && entity.TryInvokeTouchInteractionEventWith(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeTouchInteractionEventWith(this IEntity entity, in Collider2D other)
        {
            return other.TryGetComponent(out IEntity otherEntity) && entity.TryInvokeTouchInteractionEventWith(otherEntity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeTouchInteractionEventWith(this IEntity entity, in IEntity otherEntity)
        {
            if (!entity.TryGetTouchInteractionEventData(out var touchInteractionEvent)) return false;
        
            touchInteractionEvent.Invoke(otherEntity);
            return true;
        }
    }
}