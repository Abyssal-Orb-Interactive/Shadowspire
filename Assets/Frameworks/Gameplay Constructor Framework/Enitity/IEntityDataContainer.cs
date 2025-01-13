using GameplayConstructorFramework.Enitity;

namespace GameplayConstructorFramework.Entity
{
    public interface IEntityDataContainer : IReadonlyEntityDataContainer
    {
        public bool TryAddData<T>(int key, T data) where T : class;
        public bool TryRemoveData(int key);
    }
}