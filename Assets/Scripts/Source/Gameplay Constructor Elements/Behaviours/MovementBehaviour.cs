using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class MovementBehaviour : IInitBehaviour, ISleepingBehaviour, IFrameRunBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private IReadonlyAtomicReactiveProperty<IEntity> _inputHandler = null;
        private IAtomicValue<float> _speed = null;
        private IAtomicValue<Rigidbody2D> _rigidbody2D = null;
        private ReactiveLibraryFacade.IObservable<float2> _movementInputAction = null;
        private IAtomicValue<bool> _canMove = null;
        
        private float2 _direction = float2.zero;
        private IDisposable _movementInputActionSubscription = null;

        public MovementBehaviour()
        {
            _entity = new Entity();
        }

        public MovementBehaviour(IEntity entity)
        {
            _entity = entity;
        }


        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetSpeedData(out var speed);
            _speed = speed;
            
            _entity.TryGetRigidbody2DData(out var rigidbody2D);
            _rigidbody2D = rigidbody2D;
            
            _entity.TryGetCanMoveData(out var canMove);
            _canMove = canMove;
            
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
            _movementInputActionSubscription = _movementInputAction.Subscribe(OnMovementInputAction);
        }
        
        private void OnMovementInputAction(float2 newDirection)
        {
            _direction = newDirection;
        }
        
        public void OnFrameRun()
        {
            if(!_canMove.CurrentValue) return;
            
            var targetPosition = MovementCases.CalculateTargetPosition(_rigidbody2D, _direction, _speed, Time.fixedDeltaTime);
            
            _rigidbody2D.CurrentValue.MovePosition(targetPosition);        
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
            _movementInputActionSubscription?.Dispose();
        }
    }
}