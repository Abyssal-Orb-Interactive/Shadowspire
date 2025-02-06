using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace UnityExtensions
{
    public static class RigidbodyExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Obsolete")]
        public static void SetVelocityTo(this Rigidbody2D rb, float2 newVelocity)
        {
            rb.velocity = newVelocity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Obsolete")]
        public static void SetVelocityYTo(this Rigidbody2D rb, float newVelocityY)
        {
            rb.SetVelocityTo(new float2 (rb.velocity.x, newVelocityY));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Obsolete")]
        public static void SetVelocityXTo(this Rigidbody2D rb, float newVelocityX)
        {
            rb.SetVelocityTo(new float2 (newVelocityX, rb.velocity.y));
        }
    }
}