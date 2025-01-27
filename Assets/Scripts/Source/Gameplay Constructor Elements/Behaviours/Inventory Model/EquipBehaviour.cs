using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;

namespace GameplayConstructorElements.Behaviours.InventoryModel
{
    [Serializable]
    public sealed class EquipBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IAtomicVariable<IEntity> _rightHand = null;
        private IAtomicVariable<IEntity> _leftHand = null;
        private ReactiveLibraryFacade.IObservable<IEntity> _rightHandEquipActionEvent = null;
        private ReactiveLibraryFacade.IObservable<IEntity> _leftHandEquipActionEvent = null;
        
        private IDisposable _subscription = null;

        public EquipBehaviour()
        {
            _entity = new Entity();
        }

        public EquipBehaviour(IEntity entity)
        {
            _entity = entity;
        }

        public void Init()
        {
            _entity.TryGetRightHandData(out var rightHand);
            _rightHand = rightHand;
            
            _entity.TryGetLeftHandData(out var leftHand);
            _leftHand = leftHand;
            
            _entity.TryGetRightHandEquipActionEventData(out var rightHandEquipActionEvent);
            _rightHandEquipActionEvent = rightHandEquipActionEvent;
            
            _entity.TryGetLeftHandEquipActionEventData(out var leftHandEquipActionEvent);
            _leftHandEquipActionEvent = leftHandEquipActionEvent;
            
            OnInit();
        }

        public void OnInit()
        {
            
        }
        
        public void Awake()
        {
            OnAwake();
        }

        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_rightHandEquipActionEvent.Subscribe(OnRightHandEquipAction));
            subscriptionBuilder.Add(_leftHandEquipActionEvent.Subscribe(OnLeftHandEquipAction));
            
            _subscription = subscriptionBuilder.Build();
        }

        private void OnRightHandEquipAction(IEntity newItem)
        {
            if (_rightHand.CurrentValue != null)
            {
                _rightHand.CurrentValue.Dispose();
                _rightHand.Value = null;
            }
            
            _rightHand.Value = newItem;
        }
        
        private void OnLeftHandEquipAction(IEntity newItem)
        {
            if (_leftHand.CurrentValue != null)
            {
                _leftHand.CurrentValue.Dispose();
                _leftHand.Value = null;
            }
            
            _leftHand.Value = newItem;
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