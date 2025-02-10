using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using TimeFramework.Core;
using UnityEngine;

namespace TimeFramework.Timers
{
    public static class TimersFabric
    {
        private static TimeInvoker _timeInvoker;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Timer Create(in float delay, in TimerType timerType)
        {
            _timeInvoker ??= TimeInvoker.Instance;
            
            return new Timer(delay, _timeInvoker, timerType); 
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Timer Create(in IAtomicValue<float> delay, in TimerType timerType)
        {
            return Create(delay.CurrentValue, timerType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Timer CreateScaledFrame(in float delay)
        {
            return Create(delay, TimerType.ScaledFrame);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Timer CreateScaledFrame(in IAtomicValue<float> delay)
        {
            return Create(delay.CurrentValue, TimerType.ScaledFrame);
        }
    }
}