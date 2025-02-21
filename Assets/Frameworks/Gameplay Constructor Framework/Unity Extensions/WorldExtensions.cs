using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Enitity.World;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameplayConstructorFramework.Entity.Unity
{
    public static class WorldExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CreateEntity<T>(this IWorld world, T entityPrefab, Transform parent = null, float3 position = new(), Quaternion rotation = new())
            where T : Object
        {
            var go = entityPrefab switch
            {
                GameObject gameObjectPrefab => (parent != null)
                    ? Object.Instantiate(gameObjectPrefab, position, rotation, parent)
                    : Object.Instantiate(gameObjectPrefab, position, rotation),
                Component componentPrefab => (parent != null)
                    ? Object.Instantiate(componentPrefab.gameObject, position, rotation, parent)
                    : Object.Instantiate(componentPrefab.gameObject, position, rotation),
                _ => throw new ArgumentException($"Unsupported prefab type: {typeof(T)}", nameof(entityPrefab))
            };

            if (!go.TryGetComponent(out IEntity entity)) throw new InvalidOperationException("Instantiated object does not have an entity component.");
            
            world.AddEntity(entity);
            return entity.ID;

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CreateEntity<T>(this IWorld world, IAtomicValue<T> entityPrefab, Transform parent = null, float3 position = new(), Quaternion rotation = new())
            where T : Object
        {
            return CreateEntity(world, entityPrefab.CurrentValue, parent, position, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CreateEntity<T>(this IWorld world, IAtomicValue<T> entityPrefab, IAtomicValue<Transform> parent = null, float3 position = new(), Quaternion rotation = new())
            where T : Object
        {
            return CreateEntity(world, entityPrefab.CurrentValue, parent?.CurrentValue, position, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CreateEntity<T>(this IAtomicValue<IWorld> world, IAtomicValue<T> entityPrefab, IAtomicValue<GameObject> parent = null, float3 position = new(), Quaternion rotation = new())
            where T : Object
        {
            return CreateEntity(world.CurrentValue, entityPrefab.CurrentValue, parent?.CurrentValue.transform, position, rotation);
        }
    }
}