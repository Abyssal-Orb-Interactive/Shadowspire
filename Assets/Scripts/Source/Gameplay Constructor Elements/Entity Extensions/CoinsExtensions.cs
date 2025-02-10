using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using IntExtensions;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class CoinsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddCoins(this IEntity otherEntity, in int coins)
        {
            if(!otherEntity.TryGetCoinsData(out var wallet)) return true;
            wallet.IncreaseValueBy(coins);
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddCoins(this IEntity otherEntity, in IAtomicValue<int> coins)
        {
            return otherEntity.TryAddCoins(coins.CurrentValue);
        }
    }
}