using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Enitity;

namespace GameplayConstructorFramework.Entity
{
    public interface IEntity : IReadonlyEntity, IEntityDataContainer, IEntityBehavioursContainer, IDisposable
    {
        public AtomicReactiveProperty<bool> IsActive { get; set; }
        public int ID { get; set; }
    }
}