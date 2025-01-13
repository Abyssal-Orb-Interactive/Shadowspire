using System.Collections.Generic;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorFramework.Enitity.World
{
    public interface IWorld
    {
        void AddEntity(IEntity entity);
        int CreateEntity();
        bool TryGetEntityWithID(int id, out IEntity entity);
        void Destroy(int entityID);
        
        public IReadOnlyDictionary<int, IEntity> Entities { get; }
    }
}