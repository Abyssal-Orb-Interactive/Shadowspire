using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using IntExtensions;
using UseCases;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class LevelingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;

        private IAtomicVariable<int> _level = null;
        private IReadonlyAtomicReactiveProperty<float> _xpTarget = null;
        private AtomicReactiveProperty<float> _xp = null;
        
        private IDisposable _xpSubscription = null;
        private IDisposable _xpTargetSubscription = null;

        public LevelingBehaviour()
        {
            _entity = new Entity();
        }

        public LevelingBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetLevelData(out var level);
            _level = level;
            
            _entity.TryGetXpTargetData(out var xpTarget);
            _xpTarget = xpTarget;
            
            _entity.TryGetXpData(out _xp);
            
            OnInit();
        }

        public void OnInit()
        {
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }
        
        public void OnAwake()
        {
            _xpTargetSubscription = _xpTarget.Subscribe(OnXpTargetChanges);
            _xpSubscription = _xp.Subscribe(OnXpChanges);
        }

        private void OnXpTargetChanges(float newXpTarget)
        {
            if (_xp.CurrentValue < newXpTarget) return;

            LevelCases.LevelUp(_level, 1);
            _xp.Value = _xp.CurrentValue - newXpTarget;
        }

        private void OnXpChanges(float newXp)
        {
            if (_xp.CurrentValue < _xpTarget.CurrentValue) return;
            
            LevelCases.LevelUp(_level, 1);
            _xp.Value = _xp.CurrentValue - _xpTarget.CurrentValue;
        }

        public void Sleep()
        {
            OnSleep();
        }
        
        public void OnSleep()
        {
            Dispose();
        }
        
        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }
        public void Dispose()
        {
            _xpSubscription?.Dispose();
            _xpTargetSubscription?.Dispose();
        }
    }
}