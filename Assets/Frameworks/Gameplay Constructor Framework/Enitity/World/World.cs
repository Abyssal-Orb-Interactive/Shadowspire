using System;
using System.Collections.Generic;
using Base;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorFramework.Enitity.World
{
    [Serializable]
    public sealed class World : IWorld
    {
        private EntityBehaviourGameLoopService _gameLoop = null;
        private int _nextID = int.MinValue;
        private Dictionary<int, IEntity> _entities = null;

        public IReadOnlyDictionary<int, IEntity> Entities => _entities;

        public World()
        {
            _entities = new Dictionary<int, IEntity>();
        }

        public World(EntityBehaviourGameLoopService gameLoop)
        {
            _gameLoop = gameLoop;
            _entities = new Dictionary<int, IEntity>();
        }

        public void AddEntity(IEntity entity)
        {
            _entities[_nextID] = entity;
            entity.ID = _nextID;
            _nextID++;
        }

        public int CreateEntity()
        {
            var id = _nextID;
            _nextID++;
            _entities[id] = new Entity.Entity(_gameLoop);
            return id;
        }

        public bool TryGetEntityWithID(int id, out IEntity entity)
        {
            return _entities.TryGetValue(id, out entity);
        }

        public void Destroy(int entityID)
        {
            _entities[entityID].Dispose();
            _entities.Remove(entityID);
        }
    }
}