using System;
using System.Runtime.CompilerServices;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using TimeFramework.Core;
using Unity.Mathematics;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class FreeFallingBehaviour : BehaviourBase, IInitBehaviour, IPhysicsRunBehaviour
    {
        private float _currentFreeFallingSpeed = 0f;
        private TimeInvoker _timeInvoker = null;
        
        #region Cache Variables
        
        private IAtomicValue<Rigidbody2D> _rigidbody2D = null;
        private IAtomicValue<float> _maxFreeFallingSpeed = null;
        private IAtomicValue<float> _freeFallingAcceleration = null;
        private IAtomicValue<bool> _canFreeFalling = null;
        
        #endregion
        
        #region Constructors

        public FreeFallingBehaviour() {}
        public FreeFallingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion
        
        #region Life Cycle Methods
        
        public void Init()
        {
            _timeInvoker = TimeInvoker.Instance;
            
            _entity.TryGetRigidbody2DData(out var rigidbody2D);
            _rigidbody2D = rigidbody2D;
            
            _entity.TryGetMaxFreeFallingSpeedData(out var maxFreeFallingSpeed);
            _maxFreeFallingSpeed = maxFreeFallingSpeed;
            
            _entity.TryGetFreeFallingAccelerationData(out var freeFallingAcceleration);
            _freeFallingAcceleration = freeFallingAcceleration;
            
            _entity.TryGetCanFreeFallingData(out var canFreeFalling);
            _canFreeFalling = canFreeFalling;
            
            OnInit();
        }
        
        [Obsolete("Obsolete")]
        public void OnPhysicsFrameRun()
        {
            if (!_canFreeFalling.CurrentValue)
            {
                _currentFreeFallingSpeed = 0f;
                return;
            }
            
            var fallingSpeedAfterAcceleration = MovementCases.CalculateSpeedAfterAccelerationWithDeltaTime(
                _currentFreeFallingSpeed, 
                _freeFallingAcceleration, 
                _timeInvoker
                );
            
            _currentFreeFallingSpeed = math.min(fallingSpeedAfterAcceleration, _maxFreeFallingSpeed.CurrentValue);
            var rb = _rigidbody2D.CurrentValue;
            rb.velocity = new float2(rb.velocity.x, -_currentFreeFallingSpeed);
        }

        public void OnInit()
        {
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
        }
        
        #endregion
    }
}