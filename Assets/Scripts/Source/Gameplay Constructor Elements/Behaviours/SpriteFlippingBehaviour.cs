using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class SpriteFlippingBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;

        private IAtomicValue<SpriteRenderer> _spriteRenderer = null;
        private AtomicReactiveProperty<bool> _flipped = null;
        private IReadonlyAtomicReactiveProperty<IEntity> _inputHandler = null;
        private ReactiveLibraryFacade.IObservable<float2> _movementInputAction = null;
        
        private IDisposable _subscriptions = null;
        
        public SpriteFlippingBehaviour()
        {
            _entity = new Entity();
        }

        public SpriteFlippingBehaviour(IEntity entity)
        {
            _entity = entity;
        }
        public void Init()
        {
            _entity.TryGetSpriteRendererData(out var spriteRenderer);
            _spriteRenderer = spriteRenderer;
            
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;

            _entity.TryGetFlippedData(out _flipped);
            
            OnInit();
        }

        public void OnInit()
        {
            _inputHandler.CurrentValue.TryGetMovementInputActionData(out var movementInputAction);
            _movementInputAction = movementInputAction;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }
        
        public void OnAwake()
        {
            var subscriptionsBuilder = new DisposableBuilder();
            
            subscriptionsBuilder.Add(_movementInputAction.Subscribe(OnMovementInput)); 
            subscriptionsBuilder.Add(_flipped.Subscribe(OnFlip));
            
            _subscriptions = subscriptionsBuilder.Build();
        }

        private void OnMovementInput(float2 newDirection)
        {
            _flipped.Value = _flipped.CurrentValue switch
            {
                true when newDirection.x > 0.0f => false,
                false when newDirection.x < 0.0f => true,
                _ => _flipped.CurrentValue
            };
        }
        
        private void OnFlip(bool newValue)
        {
            _spriteRenderer.CurrentValue.flipX = newValue;
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
            _subscriptions?.Dispose();
        }
    }
}