using AtomicFramework.AtomicStructures;

namespace GameplayConstructorFramework.Enitity
{
    public interface IReadonlyEntity : IReadonlyEntityDataContainer, IReadonlyEntityBehavioursContainer
    {
        public AtomicReactiveProperty<bool> IsActive { get;}
        public int ID { get; }
    }
}