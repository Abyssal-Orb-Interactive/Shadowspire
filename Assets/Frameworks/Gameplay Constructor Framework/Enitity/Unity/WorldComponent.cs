using System;
using System.Collections.Generic;
using Base.UnityIntegration;
using GameplayConstructorFramework.Enitity.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameplayConstructorFramework.Entity.Unity
{
    [Serializable]
    public sealed class WorldComponent : MonoBehaviour, IWorld
    {
        [SerializeField] private GameLoopComponent _gameLoopComponent = null;
        [SerializeField] private EntitiesInitializer _entitiesInitializer = null;
        
        [ShowInInspector, FoldoutGroup("Entities Debug", true), Searchable, TableList]
        private IReadOnlyDictionary<int, IEntity> _entitiesDebug => _world.Entities;

        public IReadOnlyDictionary<int, IEntity> Entities => _world.Entities;

        private World _world = null;


        private void Awake()
        {
            _gameLoopComponent ??= GameObject.Find("Game Loop").GetComponent<GameLoopComponent>();

            _entitiesInitializer ??= GetComponent<EntitiesInitializer>();
            
            _world = new World(_gameLoopComponent.GameLoop);

            DontDestroyOnLoad(this);
            
            if(_entitiesInitializer != null) _entitiesInitializer.InitializeEntities(this);
        }

        public void AddEntity(IEntity entity)
        {
            _world.AddEntity(entity);
        }

        public int CreateEntity()
        {
            return _world.CreateEntity();
        }

        public bool TryGetEntityWithID(int id, out IEntity entity)
        {
            return _world.TryGetEntityWithID(id, out entity);
        }

        public void Destroy(int entityID)
        {
            _world.Destroy(entityID);
        }

    }
}