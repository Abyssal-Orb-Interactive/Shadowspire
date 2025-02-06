using System;
using TimeFramework.Core;
using UnityEngine;

namespace Base.UnityIntegration
{
    [Serializable]
    public sealed class GameLoopComponent : MonoBehaviour
    {
        [SerializeField] private float _tickDelta = 0f;
        
        private TimeInvoker _timeInvoker = null;
        
        private float _tickTimer = 0f;

        public EntityBehaviourGameLoopService GameLoop = new();
        private void Start()
        {
            _timeInvoker = TimeInvoker.Instance;
            GameLoop.Awake();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            GameLoop.Init();
        }

        private void OnEnable()
        {
            GameLoop.Awake();
        }

        private void Update()
        {
            _timeInvoker.UpdateTime();
            GameLoop.FrameUpdate();
            
            _tickTimer += _timeInvoker.DeltaTime;

            if (_tickTimer < _tickDelta) return;
            
            _tickTimer = 0f;
            GameLoop.TickUpdate();
        }

        private void FixedUpdate()
        {
            GameLoop.PhysicsUpdate();
        }

        private void LateUpdate()
        {
            GameLoop.EndOfFrameUpdate();
        }

        private void OnDisable()
        {
            GameLoop.Sleep();
        }

        private void OnDestroy()
        {
            GameLoop.Destroy();
        }
    }
}