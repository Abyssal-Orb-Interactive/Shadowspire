using System;
using AtomicFramework.AtomicStructures;
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
        private bool _previousFlipBuffer = false;
        
        #region Cache Variables
        
        private IReadonlyAtomicReactiveProperty<bool> _flipped = null;
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
            _entity.TryGetFlippedData(out var flipped);
            _flipped = flipped;
            
            _entity.TryGetTargetTrigger2DColliderData(out var triggerCollider);
            _triggerCollider = triggerCollider;
            
            OnInit();
        }

        public void OnInit()
        {
            _previousFlipBuffer = _flipped.CurrentValue;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _flipped.Subscribe(OnFlip);
        }

        private void OnFlip(bool flipped)
        {
            var collider = _triggerCollider.CurrentValue;
            var offset = collider.offset.ToFloat2();
            var offsetY = offset.y;
            var offsetAbsX = math.abs(offset.x);
            collider.offset = !flipped ? new float2(offsetAbsX, offsetY) : new float2(-offsetAbsX, offsetY);
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