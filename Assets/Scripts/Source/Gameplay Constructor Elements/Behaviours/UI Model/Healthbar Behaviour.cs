using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine.UI;

namespace GameplayConstructorElements.Behaviours.UI_Model
{
    [Serializable]
    public sealed class HealthBarBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicValue<Image> _healthBar = null;
        private IAtomicValue<IEntity> _displayingEntity = null;
        private ReactiveLibraryFacade.IObservable<float> _healthPercent = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public HealthBarBehaviour() {}
        public HealthBarBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetHealthBarData(out var healthBar);
            _healthBar = healthBar;
            
            _entity.TryGetDisplayingEntityData(out var displayingEntity);
            _displayingEntity = displayingEntity;
            
            OnInit();
        }

        public void OnInit()
        {
            _displayingEntity.CurrentValue.TryGetHealthPercentData(out var healthPercent);
            _healthPercent = healthPercent;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _healthPercent.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float newHealthPercent)
        {
            _healthBar.CurrentValue.fillAmount = newHealthPercent;
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