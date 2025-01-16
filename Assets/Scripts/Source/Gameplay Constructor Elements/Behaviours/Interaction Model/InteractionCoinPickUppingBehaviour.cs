using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Behaviours.Interaction_Model
{
    [Serializable]
    public sealed class InteractionCoinPickUppingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;

        private ReactiveLibraryFacade.IObservable<IEntity> _interactionActionEvent = null;
        private IAtomicVariable<int> _coins = null;
        
        private IDisposable _subscription = null;

        public InteractionCoinPickUppingBehaviour()
        {
            _entity = new Entity();
        }

        public InteractionCoinPickUppingBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetInteractionActionEventData(out var interactionActionEvent);
            _interactionActionEvent = interactionActionEvent;
            
            _entity.TryGetCoinsData(out var coins);
            _coins = coins;

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
           _subscription = _interactionActionEvent.Subscribe(OnInteractWith);
        }

        private void OnInteractWith(IEntity otherEntity)
        {
            if (!otherEntity.TryAddCoins(_coins)) return;

            _coins.Value = 0;
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
    }
}