using System;
using GameplayConstructorFramework.Entity;

namespace AtomicFramework.AtomicStructures
{
    public interface IEntityFunctionFabric<out R>
    {
        Func<R> Create(IEntity entity);
    }
    
    public interface IEntityFunctionFabric<in T, out R>
    {
        Func<T, R> Create(IEntity entity);
    }
    
    public interface IEntityFunctionFabric<in T1, in T2, out R>
    {
        Func<T1, T2, R> Create(IEntity entity);
    }
    
    public interface IEntityFunctionFabric<in T1, in T2, in T3, out R>
    {
        Func<T1, T2, T3, R> Create(IEntity entity);
    }
}