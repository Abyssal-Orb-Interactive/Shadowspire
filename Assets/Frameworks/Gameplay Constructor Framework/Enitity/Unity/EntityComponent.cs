using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using Base.UnityIntegration;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFrameworkAPIs;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace GameplayConstructorFramework.Entity.Unity
{
    public class EntityComponent : MonoBehaviour, IEntity
    {
        [SerializeField] private GameLoopComponent _gameLoopComponent = null;

        [SerializeField, FoldoutGroup("Install Pipeline", true)]
        private List<EntityInitializer> _entityInitializers = new();

        [ShowInInspector, FoldoutGroup("Entity Debug", true)]
        [TableList]
        private List<SerializedDataEntry> _dataDebug => ConvertDataToEnum();

        [ShowInInspector, FoldoutGroup("Entity Debug", true)]
        [TableList]
        private List<SerializedBehaviourEntry> _behaviourDebug => ConvertBehaviourToEnum();
        
        [SerializeField] private Entity _entity = null;

        public AtomicReactiveProperty<bool> IsActive
        {
            get => _entity.IsActive;
            set
            {
                _entity.IsActive = value;
                if (gameObject.activeSelf != value.CurrentValue)
                {
                    gameObject.SetActive(value.CurrentValue);
                }
            }
        }
        
        public int ID
        {
            get => _entity.ID;
            set => _entity.ID = value;
        }


        private List<SerializedDataEntry> ConvertDataToEnum()
        {
            var result = new List<SerializedDataEntry>();

            if (_entity?.Data == null) return result;

            foreach (var (key, value) in _entity.Data)
            {
                var enumKey = (GlobalDataAPI)key;
                result.Add(new SerializedDataEntry(enumKey, value));
            }

            return result;
        }
        
        private List<SerializedBehaviourEntry> ConvertBehaviourToEnum()
        {
            var result = new List<SerializedBehaviourEntry>();

            if (_entity?.Behaviours == null) return result;

            foreach (var (key, value) in _entity.Behaviours)
            {
                var enumKey = (GlobalBehavioursAPI)key;
                result.Add(new SerializedBehaviourEntry(enumKey, value));
            }

            return result;
        }

        private void Reset()
        {
            _entity = new Entity();
        }

        public void Awake()
        {
            _gameLoopComponent ??= GameObject.Find("Game Loop").GetComponent<GameLoopComponent>();
            
            CreateEntity();

            foreach (var initializer in _entityInitializers)
            {
                initializer.InitializeData(this);
            }        
        }
        
        private void CreateEntity()
        {
            _entity = new Entity(_gameLoopComponent.GameLoop);
        }
        
        private void Update()
        {
            RefreshInspector();
        }

        private void RefreshInspector()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) return;
            EditorUtility.SetDirty(this);
            #endif
        }
        
        private void Start()
        {
            foreach (var initializer in _entityInitializers)
            {
                initializer.InitializeBehaviours(this);
            }
        }

        private void OnEnable()
        {
            _entity.Awake();
        }

        private void OnDisable()
        {
            _entity.Sleep();
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _entity?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Serializable]
        public struct SerializedDataEntry
        {
            [TableColumnWidth(100)]
            public string Key;

            [TableColumnWidth(200)]
            public object Value;

            public SerializedDataEntry(GlobalDataAPI key, object value)
            {
                Key = key.ToString();
                Value = value;
            }
        }
        
        [Serializable]
        public struct SerializedBehaviourEntry
        {
            [TableColumnWidth(100)]
            public string Key;

            [TableColumnWidth(200)]
            public IGameLoopBehaviour Value;

            public SerializedBehaviourEntry(GlobalBehavioursAPI key, IGameLoopBehaviour value)
            {
                Key = key.ToString();
                Value = value;
            }
        }

        public bool TryAddData<T>(int key, T data) where T : class
        {
            return _entity.TryAddData(key, data);
        }

        public bool TryGetData<T>(int key, out T data) where T : class
        {
            return _entity.TryGetData(key, out data);
        }

        public bool TryRemoveData(int key)
        {
            return _entity.TryRemoveData(key);
        }

        public bool HasData(int key)
        {
            return _entity.HasData(key);
        }

        public bool TryGetBehaviour<T>(int key, out T behaviour) where T : IGameLoopBehaviour
        {
            return _entity.TryGetBehaviour(key, out behaviour);
        }

        public bool TryAddBehaviour<T>(int key, T behaviour) where T : IGameLoopBehaviour
        {
            return _entity.TryAddBehaviour(key, behaviour);
        }

        public bool TryRemoveBehaviour<T>(int key) where T : IGameLoopBehaviour
        {
            return _entity.TryRemoveBehaviour<T>(key);
        }

        public bool HasBehaviour(int key)
        {
           return _entity.HasBehaviour(key);
        }
    }
}