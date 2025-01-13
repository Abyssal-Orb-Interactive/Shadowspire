using ReactiveLibraryFacade;

namespace AtomicFramework.AtomicStructures
{
    public interface IReadonlyAtomicReactiveProperty<out T> : IAtomicValue<T>, IObservable<T>
    {
        
    }
}