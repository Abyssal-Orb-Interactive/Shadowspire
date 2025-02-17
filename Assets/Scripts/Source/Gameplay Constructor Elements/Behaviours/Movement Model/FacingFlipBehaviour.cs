using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class FacingFlipBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private ReactiveLibraryFacade.IObservable<Facing> _currentFacing = null;
        private IAtomicValue<Facing> _originFacing = null;
        private IAtomicVariable<bool> _isFlipped = null;

        #endregion

        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public FacingFlipBehaviour() {}
        
        public FacingFlipBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetOriginFacingData(out var originFacing);
            _originFacing = originFacing;
            
            _entity.TryGetCurrentFacingData(out var currentFacing);
            _currentFacing = currentFacing;
            
            _entity.TryGetFlippedData(out var isFlipped);
            _isFlipped = isFlipped;
            
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
            _subscription = _currentFacing.Subscribe(OnFacingChanged);
        }

        private void OnFacingChanged(Facing newFacing)
        {
            _isFlipped.Value = _originFacing.CurrentValue != newFacing;
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