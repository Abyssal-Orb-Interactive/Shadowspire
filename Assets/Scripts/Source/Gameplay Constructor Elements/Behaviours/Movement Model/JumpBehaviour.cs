using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using ReactiveLibraryFacade.DataStructures;
using Unity.Mathematics;
using UnityEngine;
using UnityExtensions;
using UseCases;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class JumpBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables

        private IAtomicValue<float> _jumpHeight = null;
        private ReactiveLibraryFacade.IObservable<float> _jumpInputAction = null;
        private IAtomicValue<IEntity> _inputHandler = null;
        private IAtomicValue<Rigidbody2D> _rigidbody = null;
        private IAtomicValue<bool> _canJump = null;
        private IAtomicVariable<bool> _isJumping = null;
        private AtomicReactiveProperty<bool> _isGrounded;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public JumpBehaviour() {}

        public JumpBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetJumpHeightData(out var jumpHeight);
            _jumpHeight = jumpHeight;
            
            _entity.TryGetRigidbody2DData(out var rigidbody);
            _rigidbody = rigidbody;
            
            _entity.TryGetCanJumpData(out var canJump);
            _canJump = canJump;
            
            _entity.TryGetIsJumpingData(out var isJumping);
            _isJumping = isJumping;
            
            _entity.TryGetIsGroundedData(out var isGrounded);
            _isGrounded = isGrounded;
            
            OnInit();
        }

        public void OnInit()
        {
            _inputHandler.CurrentValue.TryGetJumpInputActionData(out var jumpInputAction);
            _jumpInputAction = jumpInputAction;
        }
        
        [Obsolete("Obsolete")]
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        [Obsolete("Obsolete")]
        public void OnAwake()
        {
            var subscriptionBuilder = new DisposableBuilder();
            
            subscriptionBuilder.Add(_jumpInputAction.Subscribe(OnJumpInputAction));
            subscriptionBuilder.Add(_isGrounded.Subscribe(OnIsGroundedChanged));
            
            _subscription = subscriptionBuilder.Build();
        }

        [Obsolete("Obsolete")]
        private void OnJumpInputAction(float jumpInputModifier)
        {
            if (!_canJump.CurrentValue) return;

            var rb = _rigidbody.CurrentValue;
            
            var gravity = math.abs(Physics2D.gravity.y * rb.gravityScale);
            var correctedHeight = _jumpHeight.CurrentValue * jumpInputModifier;
            
            var jumpVelocity = MovementCases.CalculateJumpVelocity(gravity, correctedHeight);

            _isJumping.Value = true;
            if(_isGrounded.CurrentValue == true) _isGrounded.Value = false;
            
            rb.SetVelocityYTo(jumpVelocity);
        }
        
        private void OnIsGroundedChanged(bool isGrounded)
        {
            if(!isGrounded || !_isJumping.CurrentValue) return;
            
            _isJumping.Value = false;
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