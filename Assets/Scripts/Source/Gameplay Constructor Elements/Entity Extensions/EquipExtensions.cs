using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class EquipExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySwitchRightHandItemWith(this IEntity entity, in IEntity newItem)
        {
            if(!entity.TryGetRightHandData(out var rightHand)) return false;
                
            rightHand.SwitchItemTo(newItem);

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySwitchLeftHandHandItemWith(this IEntity entity, in IEntity newItem)
        {
            if(!entity.TryGetLeftHandData(out var leftHand)) return false;
                
            leftHand.SwitchItemTo(newItem);

            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwitchItemTo(this IAtomicVariable<IEntity> hand, in IEntity newItem)
        {
            if (hand.CurrentValue != null)
            {
                hand.CurrentValue.Dispose();
                hand.Value = null;
            }

            hand.Value = newItem;
        }
    }
}