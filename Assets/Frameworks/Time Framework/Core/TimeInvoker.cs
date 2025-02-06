using System;
using AtomicFramework.AtomicStructures;
using ReactiveLibraryFacade;
using UnityEngine;

namespace TimeFramework.Core
{
    [Serializable]
    public sealed class TimeInvoker : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public float ONE_SECOND { get; private set; } = 1.0f;

        private static TimeInvoker _instance = null;

        public static TimeInvoker Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = new GameObject("Time Invoker").AddComponent<TimeInvoker>();
                DontDestroyOnLoad(_instance);
                
                return _instance;
            }
        }

        #region Timer Varaibles 

        private float _oneSecondTime = 0f;
        private float _unscaledOneSecondTime = 0f;

        #endregion
        
        #region Events
        
        [SerializeField] private AtomicEvent _frameUpdated = new();
        [SerializeField] private AtomicEvent _unscaledFrameUpdate = new();
        [SerializeField] private AtomicEvent _secondUpdate = new();
        [SerializeField] private AtomicEvent _unscaledSecondUpdate = new();

        #endregion
        
        #region Public Observables
        
        public IObservable FrameUpdated => _frameUpdated;
        public IObservable UnscaledFrameUpdate => _unscaledFrameUpdate;
        public IObservable SecondUpdate => _secondUpdate;
        public IObservable UnscaledSecondUpdate => _unscaledSecondUpdate;
        
        #endregion
        
        #region Unity Time
        
        public float DeltaTime => Time.deltaTime;
        public float UnscaledDeltaTime => Time.unscaledDeltaTime;
        public float TimeScale => Time.timeScale;
        public float FixedDeltaTime => Time.fixedDeltaTime;
        #endregion

        public void UpdateTime()
        {
            _frameUpdated.Invoke();
            
            _oneSecondTime += DeltaTime;
            
            if (_oneSecondTime >= ONE_SECOND)
            {
                _oneSecondTime -= ONE_SECOND;
                _secondUpdate.Invoke();
            }
            
            _unscaledFrameUpdate.Invoke();
            
            _unscaledOneSecondTime += UnscaledDeltaTime;
            
            if(_unscaledOneSecondTime < ONE_SECOND) return;
            
            _unscaledOneSecondTime -= ONE_SECOND;
            _unscaledSecondUpdate.Invoke();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _frameUpdated.Dispose();
            _unscaledFrameUpdate.Dispose();
            _secondUpdate.Dispose();
            _unscaledSecondUpdate.Dispose();
            _instance = null;
        }
    }
}