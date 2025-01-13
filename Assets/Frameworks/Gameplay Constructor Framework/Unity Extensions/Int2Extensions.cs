using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace GameplayConstructorFramework.UnityExtensions
{
    public static class Int2Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ToInt3(this int2 int2)
        {
            return new int3(int2.x, int2.y, 0);
        }
    }
}