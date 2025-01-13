using GameplayConstructor.Enitity.Behaviours;

namespace GameplayConstructorFramework.Enitity
{
    public interface IReadonlyEntityBehavioursContainer
    {
        public bool TryGetBehaviour<T>(int key, out T behaviour) where T : IGameLoopBehaviour;
        public bool HasBehaviour(int key);
    }
}