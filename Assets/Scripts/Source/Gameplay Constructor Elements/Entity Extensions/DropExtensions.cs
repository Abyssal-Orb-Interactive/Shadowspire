using System.Runtime.CompilerServices;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class DropExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryDropAllInventory(this IEntity entity)
        {
            if(!entity.TryGetItemsRegisterHolderData(out var itemsRegisterHolder)) return false;
            if(!entity.TryGetInventoryData(out var inventory)) return false;
            if(!itemsRegisterHolder.CurrentValue.TryGetItemsRegisterData(out var itemsRegister)) return false;
            
            float3 dropPosition;

            if(entity.TryGetTransformData(out var transform)) dropPosition = transform.CurrentValue.position;
            else dropPosition = float3.zero;
            
            foreach (var (id, quantity) in inventory)
            {
                if (!itemsRegister.TryGetValue(id, out var itemPrefab)) continue;
                if (itemPrefab is not Component entityComponent) continue;
                
                for (var i = 0; i < quantity; i++)
                {
                    Object.Instantiate(entityComponent.gameObject, dropPosition, Quaternion.identity);
                }
            }
            
            inventory.Clear();
            
            return true;
        }
    }
}