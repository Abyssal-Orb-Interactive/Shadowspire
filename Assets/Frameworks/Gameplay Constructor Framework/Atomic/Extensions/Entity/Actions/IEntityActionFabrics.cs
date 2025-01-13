using System;
using GameplayConstructorFramework.Entity;

namespace AtomicFramework.AtomicStructures.Actions
{
    public interface IEntityActionFabric
    {
        public Action CreateFor(IEntity entity);
    }
    
    public interface IEntityActionFabric<in T>
    {
        public Action<T> CreateFor(IEntity entity);
    }
    
    public interface IEntityActionFabric<in T1, in T2>
    {
        public Action<T1, T2> CreateFor(IEntity entity);
    }
    
    public interface IEntityActionFabric<in T1, in T2, in T3>
    {
        public Action<T1, T2, T3> CreateFor(IEntity entity);
    }
}