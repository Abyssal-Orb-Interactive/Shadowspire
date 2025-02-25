using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Behaviours.UIModel
{
    [Serializable]
    public sealed class EscapeFromWindowBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicVariable<IEntity> _currentlyOpenedWindow = null;
        private IAtomicValue<IEntity> _inputHandler = null;
        private ReactiveLibraryFacade.IObservable _escapeEvent = null;

        #endregion

        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion

        #region Constructors

        public EscapeFromWindowBehaviour() {}
        public EscapeFromWindowBehaviour(IEntity entity) : base(entity) {}

        #endregion
        
        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetCurrentlyOpenWindowData(out var openedWindow);
            _currentlyOpenedWindow = openedWindow;
            
            OnInit();
        }

        public void OnInit()
        {
            _inputHandler.CurrentValue.TryGetEscapeInputActionData(out var escapeInputAction);
            _escapeEvent = escapeInputAction;
        }

        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _escapeEvent.Subscribe(OnEscape);
        }

        private void OnEscape()
        {
            if(_currentlyOpenedWindow.CurrentValue == null) return;
            
            _currentlyOpenedWindow.CurrentValue.IsActive.Value = false;
            _currentlyOpenedWindow.Value = null;
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