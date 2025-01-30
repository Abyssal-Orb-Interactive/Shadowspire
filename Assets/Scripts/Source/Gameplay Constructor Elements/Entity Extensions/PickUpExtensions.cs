using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class PickUpExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPickUp(this IEntity entity, in IEntity pickUppingEntity)
        {
            if(!entity.TryGetInventoryData(out var inventory)) return false;
            if(!pickUppingEntity.TryGetNameData(out var nameProperty)) return false;
            
            pickUppingEntity.TryGetQuantityData(out var quantityProperty);

            var name = nameProperty.CurrentValue;
            var quantity = quantityProperty?.CurrentValue ?? 1;
            
            if (!inventory.TryAdd(name, quantity))
            {
                inventory[name] += quantity;
            }

            pickUppingEntity.Dispose();
            return true;
        }
    }
}