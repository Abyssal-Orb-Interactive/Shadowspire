using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class MovementModelDataInstaller : IEntityAtomicElementInstaller
    {
        [Header("Base")]
        [SerializeField] private AtomicReactiveProperty<Rigidbody2D> _rigidbody2D = new();
        
        [Space(10f), Header("Movement")]
        [SerializeField] private AtomicReactiveProperty<float> _speed = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isMoving = new();
        [SerializeReference] private IEntityFloatExpressionElementFabric[] _speedExpressionElementsFabrics = Array.Empty<IEntityFloatExpressionElementFabric>();
        [SerializeReference] private IEntityConditionFabric[] _canMoveConditionsFabtics = Array.Empty<IEntityConditionFabric>();
        
        [Space(10f), Header("Jumping")]
        [SerializeField] private AtomicReactiveProperty<float> _jumpHeight = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isGrounded = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isJumping = new();
        [SerializeReference] private IEntityConditionFabric[] _canJumpConditionsFabtics = Array.Empty<IEntityConditionFabric>();
        
        [Space(10f), Header("Free Falling")]
        [SerializeField] private AtomicReactiveProperty<float> _maxFreeFallingSpeed = new();
        [SerializeField] private AtomicReactiveProperty<float> _freeFallingAcceleration = new();
        [SerializeReference] private IEntityConditionFabric[] _canFreeFallingConditionsFabtics = Array.Empty<IEntityConditionFabric>();
        
        [Space(10f), Header("Jump Hanging")]
        [SerializeField] private AtomicReactiveProperty<bool> _isJumpHanging = new();
        [SerializeField] private AtomicReactiveProperty<float> _velocityThresholdForJumpHanging = new();
        [SerializeField] private AtomicReactiveProperty<float> _jumpHangingDuration = new();
        [SerializeField] private AtomicReactiveProperty<float> _jumpHangingSpeedupModifier = new();
        [SerializeField] private AtomicReactiveProperty<float> _jumpHangingSlowModifier = new();
        [SerializeReference] private IEntityConditionFabric[] _canJumpHangingConditionsFabtics = Array.Empty<IEntityConditionFabric>();
        
        [Space(10f), Header("Jump Hanging Movement Speed Boost")]
        [SerializeField] private AtomicReactiveProperty<float> _jumpHangingMovementSpeedupModifier = new();
        [SerializeField] private AtomicReactiveProperty<float> _jumpHangingMovementSpeedupBonusDuration = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddRigidbody2DData(_rigidbody2D);
            entity.TryAddSpeedData(_speed);
            entity.TryAddJumpHeightData(_jumpHeight);
            entity.TryAddIsGroundedData(_isGrounded);
            entity.TryAddIsJumpingData(_isJumping);
            entity.TryAddMaxFreeFallingSpeedData(_maxFreeFallingSpeed);
            entity.TryAddFreeFallingAccelerationData(_freeFallingAcceleration);
            entity.TryAddIsJumpHangingData(_isJumpHanging);
            entity.TryAddVelocityThresholdToJumpHangingData(_velocityThresholdForJumpHanging);
            entity.TryAddJumpHangingDurationData(_jumpHangingDuration);
            entity.TryAddJumpSpeedupModifierData(_jumpHangingSpeedupModifier);
            entity.TryAddJumpSlowModifierData(_jumpHangingSlowModifier);
            entity.TryAddJumpHangingMovementSpeedupModifierData(_jumpHangingMovementSpeedupModifier);
            entity.TryAddJumpHangingMovementSpeedupBonusDurationData(_jumpHangingMovementSpeedupBonusDuration);
            entity.TryAddIsMovingData(_isMoving);

            var speedExpression = new AtomicFloatMultiplication();
            speedExpression.AppendBy(_speedExpressionElementsFabrics, entity);
            entity.TryAddSpeedExpressionData(speedExpression);
            
            var canMoveCondition = new AtomicBoolMultiplication();
            canMoveCondition.AppendBy(_canMoveConditionsFabtics, entity);
            entity.TryAddCanMoveData(canMoveCondition);
            
            var canJumpCondition = new AtomicBoolMultiplication();
            canJumpCondition.AppendBy(_canJumpConditionsFabtics, entity);
            entity.TryAddCanJumpData(canJumpCondition);
            
            var canFreeFallingCondition = new AtomicBoolMultiplication();
            canFreeFallingCondition.AppendBy(_canFreeFallingConditionsFabtics, entity);
            entity.TryAddCanFreeFallingData(canFreeFallingCondition);
            
            var canJumpHangingCondition = new AtomicBoolMultiplication();
            canJumpHangingCondition.AppendBy(_canJumpHangingConditionsFabtics, entity);
            entity.TryAddCanJumpHangingData(canJumpHangingCondition);
        }
    }
}