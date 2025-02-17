using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using UseCases;

namespace GameplayConstructorElements.Behaviours.Health_Model
{
    [Serializable]
    public sealed class HealthPercentCalculationBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IReadonlyAtomicReactiveProperty<float> _health = null;
        private IReadonlyAtomicReactiveProperty<float> _maxHealth = null;
        private IAtomicVariable<float> _healthPercent = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public HealthPercentCalculationBehaviour() {}
        public HealthPercentCalculationBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetHealthData(out var health);
            _health = health;
            
            _entity.TryGetMaxHealthData(out var maxHealth);
            _maxHealth = maxHealth;
            
            _entity.TryGetHealthPercentData(out var healthPercent);
            _healthPercent = healthPercent;
            
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
            var subscriptionsBuilder = new DisposableBuilder();
            
            subscriptionsBuilder.Add(_health.Subscribe(OnHealthChanged));
            subscriptionsBuilder.Add(_maxHealth.Subscribe(OnMaxHealthChanged));
            
            _subscription = subscriptionsBuilder.Build();
        }

        private void OnMaxHealthChanged(float newMaxHealth)
        {
            HealthCases.SetNewHealthPercent(_health, newMaxHealth, _healthPercent);

        }

        private void OnHealthChanged(float newHealth)
        {
            HealthCases.SetNewHealthPercent(newHealth, _maxHealth, _healthPercent);
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
            _subscription = null;
        }
        
        #endregion
    }
}