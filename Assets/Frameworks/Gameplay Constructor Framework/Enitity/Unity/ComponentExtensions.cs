using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameplayConstructorFramework.Entity.Unity
{
    public static class TryGetEntityUnityClassesExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity entity)
        {
            return component.TryGetComponent(out entity);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this GameObject gameObject, out IEntity entity)
        {
            return gameObject.TryGetComponent(out entity);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity entity)
        {
            return collision.gameObject.TryGetComponent(out entity);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2d, out IEntity entity)
        {
            return collision2d.gameObject.TryGetComponent(out entity);
        }
    }
}