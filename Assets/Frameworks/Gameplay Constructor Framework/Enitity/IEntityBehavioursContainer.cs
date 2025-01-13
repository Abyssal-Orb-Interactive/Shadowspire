using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Enitity;

namespace GameplayConstructorFramework.Entity
{
    public interface IEntityBehavioursContainer : IReadonlyEntityBehavioursContainer
    {
        public bool TryAddBehaviour<T>(int key, T behaviour) where T : IGameLoopBehaviour;
        public bool TryRemoveBehaviour<T>(int key) where T : IGameLoopBehaviour;
    }
}