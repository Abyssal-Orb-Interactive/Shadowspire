using System;
using GameplayConstructorFramework.Entity;

namespace AtomicFramework.AtomicStructures.Actions
{
    public static class ActionExtensions
    {
        public static IDisposable[] SubscribeBy(this AtomicEvent atomicEvent, IEntityActionFabric[] actionFabrics,
            IEntity entity)
        {
            var count = actionFabrics.Length;
            var subscribes = new IDisposable[count];

            if(count == 0) return subscribes;

            
            for (var i = 0; i < count; i++)
            {
                subscribes[i] = atomicEvent.Subscribe(actionFabrics[i].CreateFor(entity));
            }

            return subscribes;
        }
        
        public static IDisposable[] SubscribeBy<T>(this AtomicEvent<T> atomicEvent, IEntityActionFabric<T>[] actionFabrics,
            IEntity entity)
        {
            var count = actionFabrics.Length;
            var subscribes = new IDisposable[count];

            if(count == 0) return subscribes;

            
            for (var i = 0; i < count; i++)
            {
                subscribes[i] = atomicEvent.Subscribe(actionFabrics[i].CreateFor(entity));
            }

            return subscribes;
        }
        
        public static IDisposable[] SubscribeBy<T1, T2, T3>(this AtomicEvent<T1, T2, T3> atomicEvent, IEntityActionFabric<T1, T2, T3>[] actionFabrics,
            IEntity entity)
        {
            var count = actionFabrics.Length;
            var subscribes = new IDisposable[count];

            if(count == 0) return subscribes;

            
            for (var i = 0; i < count; i++)
            {
                subscribes[i] = atomicEvent.Subscribe(actionFabrics[i].CreateFor(entity));
            }

            return subscribes;
        }
    }
}