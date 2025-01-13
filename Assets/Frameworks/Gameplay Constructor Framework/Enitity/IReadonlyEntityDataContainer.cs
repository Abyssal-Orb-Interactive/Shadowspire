namespace GameplayConstructorFramework.Enitity
{
    public interface IReadonlyEntityDataContainer
    {
        public bool TryGetData<T>(int key, out T data) where T : class;

        public bool HasData(int key);
    }
}