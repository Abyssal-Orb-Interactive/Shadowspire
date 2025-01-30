using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Behaviours.DeathModel
{
    [Serializable]
    public sealed class DeathBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables
        
        private IAtomicAction _deathEvent = null;
        private ReactiveLibraryFacade.IObservable<float> _health = null;
        
        #endregion

        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion
        
        #region Constructors
        
        public DeathBehaviour() {}
        public DeathBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetDeathEventData(out var deathEvent);
            _deathEvent = deathEvent;
            
            _entity.TryGetHealthData(out var health);
            _health = health;
            
            OnInit();
        }

        public void OnInit() {}
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
           _subscription = _health.Subscribe(OnHealthChanged);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnHealthChanged(float newHealth)
        {
            if(newHealth > 0f) return;
            
            _deathEvent.Invoke();
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
            _subscription?.Dispose();
        }
        
        #endregion
    }
}