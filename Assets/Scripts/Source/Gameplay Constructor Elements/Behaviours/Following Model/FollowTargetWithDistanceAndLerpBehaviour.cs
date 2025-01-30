using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.UnityExtensions;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.Following_Model
{
    [Serializable]
    public sealed class FollowTargetWithDistanceAndLerpBehaviour : BehaviourBase, IInitBehaviour, IFrameRunBehaviour
    {
        #region Cache Variables

        private IReadonlyAtomicReactiveProperty<IEntity> _target = null;
        private IReadonlyAtomicReactiveProperty<float3> _distance = null;
        private IAtomicValue<float> _speed = null;
        private IAtomicValue<Transform> _transform = null;
        private IAtomicValue<Transform> _targetTransform = null;

        #endregion

        #region Construcors

        public FollowTargetWithDistanceAndLerpBehaviour() {}
        public FollowTargetWithDistanceAndLerpBehaviour(IEntity entity) : base(entity) {}
        
        #endregion
        
        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetTargetForFollowingData(out var target);
            _target = target;
            
            _entity.TryGetFollowingDistanceData(out var distance);
            _distance = distance;
            
            _entity.TryGetTransformData(out var transform);
            _transform = transform;
            
            _entity.TryGetSpeedData(out var speed);
            _speed = speed;
            
            OnInit();
        }

        public void OnInit()
        {
            _target.CurrentValue.TryGetTransformData(out var transform);
            _targetTransform = transform;
        }
        
        public void OnFrameRun()
        {
            var targetPosition = _targetTransform.CurrentValue.position.ToFloat3() - _distance.CurrentValue;
            var currentPosition = _transform.CurrentValue.position.ToFloat3();
            
            var maxStep = _speed.CurrentValue * Time.deltaTime;
            var distance = math.distance(currentPosition, targetPosition);
            
            var step = distance > maxStep ? maxStep / distance : 1f;  
            
            _transform.CurrentValue.position = math.lerp(currentPosition, targetPosition, step);
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