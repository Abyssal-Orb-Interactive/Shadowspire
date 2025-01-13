using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using Base;
using GameplayConstructor.Enitity.Behaviours;
using UnityEngine;

namespace GameplayConstructorFramework.Entity
{
    [Serializable]
    public sealed class Entity : IEntity
    {
        public Dictionary<int, object> Data = new();
        public Dictionary<int, IGameLoopBehaviour> Behaviours = new();

        private EntityBehaviourGameLoopService _gameLoop = null;

        [field: SerializeField]
        public AtomicReactiveProperty<bool> IsActive { get; set; } = new(true);

        [field: SerializeField] public int ID { get; set; } = 0;

        public Entity() {}
        
        public Entity(EntityBehaviourGameLoopService gameLoop)
        {
            _gameLoop = gameLoop;
        }

        public bool TryGetData<T>(int key, out T data) where T : class
        {
            var result = Data.TryGetValue(key, out var objectData);
            data = (T)objectData;
            return result;
        }

        public bool TryAddData<T>(int key, T data) where T : class
        {
            Data[key] = data;
            return true;
        }

        public bool TryRemoveData(int key)
        {
            if(Data[key] is IDisposable disposable) disposable.Dispose();
            
            return Data.Remove(key);
        }

        public bool HasData(int key)
        {
            return Data.ContainsKey(key);
        }

        public bool TryGetBehaviour<T>(int key, out T behaviour) where T : IGameLoopBehaviour
        {
            behaviour = (T)Behaviours[key];
            return true;
        }

        public bool TryAddBehaviour<T>(int key, T behaviour) where T : IGameLoopBehaviour
        {
            Behaviours[key] = behaviour;
            _gameLoop.Add(Behaviours[key]);
            return true;
        }

        public bool TryRemoveBehaviour<T>(int key) where T : IGameLoopBehaviour
        {
            _gameLoop.Remove(Behaviours[key]);
            return Behaviours.Remove(key);
        }

        public bool HasBehaviour(int key)
        {
            return Behaviours.ContainsKey(key);
        }

        public void Awake()
        {
            IsActive.Value = true;
            
            foreach (var behaviour in Behaviours.Values)
            {
                if(behaviour is ISleepingBehaviour sleepingBehaviour) sleepingBehaviour.Awake();
            }
            
            AddAllBehavioursToGameLoop();
        }
        
        public void Sleep()
        {
            IsActive.Value = false;
            
            foreach (var behaviour in Behaviours.Values)
            {
                if(behaviour is ISleepingBehaviour sleepingBehaviour) sleepingBehaviour.Sleep();
            }
            
            RemoveAllBehavioursFromGameLoop();
        }
        
        public void Dispose()
        {
            IsActive.Value = false;
            
            foreach (var behaviour in Behaviours.Values)
            {
                if(behaviour is IInitBehaviour initBehaviour) initBehaviour.Destroy();
            }

            foreach (var data in Data.Values)
            {
                if(data is IDisposable resource) resource.Dispose(); 
            }

            RemoveAllBehavioursFromGameLoop();
        }

        private void RemoveAllBehavioursFromGameLoop()
        {
            foreach (var behaviour in Behaviours.Values)
            {
                _gameLoop.Remove(behaviour);
            }
        }
        
        private void AddAllBehavioursToGameLoop()
        {
            foreach (var behaviour in Behaviours.Values)
            {
                _gameLoop.Add(behaviour);
            }
        }
    }
}