using System;
using AtomicFramework.AtomicStructures;
using ReactiveLibraryFacade;
using TimeFramework.Core;
using UnityEngine;

namespace TimeFramework.Timers
{
    [Serializable]
    public sealed class Timer : IDisposable
    {
        private TimeInvoker _invoker;
        private readonly TimerType _type = TimerType.ScaledSecond;
        private AtomicReactiveProperty<bool> _isPaused = new();
        private AtomicReactiveProperty<float> _elapsedTimeInSeconds = new();
        private AtomicReactiveProperty<float> _remainingTimeInSeconds = new();
        
        #region Events

        [SerializeField] private AtomicEvent _timerFinished = new();
        
        #endregion

        #region Observables

        public IObservable TimerFinished => _timerFinished;
        public IReadonlyAtomicReactiveProperty<bool> IsPaused => _isPaused;
        public IReadonlyAtomicReactiveProperty<float> ElapsedTimeInSeconds => _elapsedTimeInSeconds;
        public IReadonlyAtomicReactiveProperty<float> RemainingTimeInSeconds => _remainingTimeInSeconds;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion

        public float DelayTimeInSeconds { get; private set; } = 0f;
        
        public float ElapsedTimeInPercents => ElapsedTimeInSeconds.CurrentValue / DelayTimeInSeconds;

        public Timer(float delayTimeInSeconds, TimeInvoker invoker, TimerType type)
        {
            if(invoker == null) throw new ArgumentNullException(nameof(invoker), "TimeInvoker cannot be null.");;
            
            _invoker = invoker;
            _type = type;
            
            DelayTimeInSeconds = delayTimeInSeconds;
            _elapsedTimeInSeconds.Value = 0f;
        }
        
        public Timer(IAtomicValue<float> delayTimeInSeconds, TimeInvoker invoker, TimerType type)
        {
            if(invoker == null) throw new ArgumentNullException(nameof(invoker), "TimeInvoker cannot be null.");;
            
            _invoker = invoker;
            _type = type;
            
            DelayTimeInSeconds = delayTimeInSeconds.CurrentValue;
            _elapsedTimeInSeconds.Value = 0f;
        }

        public void Start()
        {
            Resume();
        }

        private void Resume()
        {
            if(!_isPaused.CurrentValue) return;
            
            _isPaused.Value = false;
            StartListeningTimeUpdates();
        }

        private void StartListeningTimeUpdates()
        {
            _subscription = _type switch
            {
                TimerType.ScaledSecond => _invoker.SecondUpdate.Subscribe(OnSecondTimerTick),
                TimerType.UnscaledSecond => _invoker.UnscaledSecondUpdate.Subscribe(OnSecondTimerTick),
                TimerType.ScaledFrame => _invoker.UnscaledFrameUpdate.Subscribe(OnFrameTimerTick),
                TimerType.UnscaledFrame => _invoker.UnscaledFrameUpdate.Subscribe(OnUnscaledFrameTimerTick),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnSecondTimerTick()
        {
            _elapsedTimeInSeconds.Value += _invoker.ONE_SECOND;
            _remainingTimeInSeconds.Value -= _invoker.ONE_SECOND;

            if (_elapsedTimeInSeconds.CurrentValue < DelayTimeInSeconds) return;
            
            _timerFinished.Invoke();
            Stop();
        }

        private void OnFrameTimerTick()
        {
            _elapsedTimeInSeconds.Value += _invoker.DeltaTime;
            _remainingTimeInSeconds.Value -= _invoker.DeltaTime;

            if (_elapsedTimeInSeconds.CurrentValue < DelayTimeInSeconds) return;
            
            _timerFinished.Invoke();
            Stop();        
        }
        
        private void OnUnscaledFrameTimerTick()
        {
            _elapsedTimeInSeconds.Value += _invoker.UnscaledDeltaTime;
            _remainingTimeInSeconds.Value -= _invoker.UnscaledDeltaTime;
            
            if (_elapsedTimeInSeconds.CurrentValue < DelayTimeInSeconds) return;
            
            _timerFinished.Invoke();
            Stop();
        }

        public void Pause()
        {
            if(_isPaused.CurrentValue) return;
            _isPaused.Value = true;
            _subscription?.Dispose();
        }

        public void Stop()
        {
            Pause();
            _elapsedTimeInSeconds.Value = 0f;
        }

        public void Restart()
        {
            Stop();
            Start();
        }
        public void Dispose()
        {
            Stop();
            _timerFinished.Dispose();
            _isPaused.Dispose();
            _remainingTimeInSeconds.Dispose();
            _elapsedTimeInSeconds.Dispose();
        }
    }
}