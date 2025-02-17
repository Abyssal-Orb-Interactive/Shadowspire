using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.UnityExtensions;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class TargetsTriggerFlippingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables
        
        private IReadonlyAtomicReactiveProperty<Facing> _currentFacing = null;
        private IAtomicValue<Collider2D> _triggerCollider = null;

        #endregion
        
        #region Subscriptions

        private IDisposable _subscription = null;
        
        #endregion
        
        #region Constructors

        public TargetsTriggerFlippingBehaviour() {}
        public TargetsTriggerFlippingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetCurrentFacingData(out var currentFacing);
            _currentFacing = currentFacing;
            
            _entity.TryGetTargetTrigger2DColliderData(out var triggerCollider);
            _triggerCollider = triggerCollider;
            
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
            _subscription = _currentFacing.Subscribe(OnFacingChanged);
        }

        private void OnFacingChanged(Facing newFacing)
        {
            var collider = _triggerCollider.CurrentValue;
            var offset = collider.offset.ToFloat2();
            var offsetY = offset.y;
            var offsetAbsX = math.abs(offset.x);
            
            collider.offset = newFacing == Facing.Left ? new float2(-offsetAbsX, offsetY) : new float2(offsetAbsX, offsetY);
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