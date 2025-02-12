using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;
using UnityExtensions;
using UseCases;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class MovementBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IPhysicsRunBehaviour, IDisposable
    {
        private float2 _direction = float2.zero;

        #region Cache Variables

        private IReadonlyAtomicReactiveProperty<IEntity> _inputHandler = null;
        private IAtomicValue<float> _speed = null;
        private IAtomicValue<Rigidbody2D> _rigidbody2D = null;
        private ReactiveLibraryFacade.IObservable<float2> _movementInputAction = null;
        private IAtomicValue<bool> _canMove = null;
        private IAtomicVariable<bool> _isMoving = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _movementInputActionSubscription = null;
        
        #endregion
        
        #region Constructors
        public MovementBehaviour() {}
        public MovementBehaviour(IEntity entity) : base(entity) {}
        
        #endregion
        
        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetSpeedExpressionData(out var speed);
            _speed = speed;
            
            _entity.TryGetRigidbody2DData(out var rigidbody2D);
            _rigidbody2D = rigidbody2D;
            
            _entity.TryGetCanMoveData(out var canMove);
            _canMove = canMove;
            
            _entity.TryGetIsMovingData(out var isMoving);
            _isMoving = isMoving;
            
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
        
        [Obsolete("Obsolete")]
        public void OnPhysicsFrameRun()
        {
            if (!_canMove.CurrentValue)
            {
                _isMoving.Value = false;
                return;
            }
            
            var rb = _rigidbody2D.CurrentValue;
            
            if (_direction.x == 0f)
            {
                rb.SetVelocityXTo(0f);
                _isMoving.Value = false;
                return;
            }

            rb.SetVelocityXTo(_direction.x * _speed.CurrentValue);
            _isMoving.Value = true;
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
            _movementInputActionSubscription = null;
        }
        
        #endregion
    }
}