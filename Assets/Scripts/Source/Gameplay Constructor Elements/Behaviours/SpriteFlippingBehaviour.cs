using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class SpriteFlippingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Varables
        
        private IAtomicValue<SpriteRenderer> _spriteRenderer = null;
        private AtomicReactiveProperty<bool> _flipped = null;

        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscriptions = null;
        
        #endregion
        
        #region Constructors
        public SpriteFlippingBehaviour() {}
        public SpriteFlippingBehaviour(IEntity entity) : base(entity) {}
        
        #endregion
        
        #region Life Cycle Methods
        public void Init()
        {
            _entity.TryGetSpriteRendererData(out var spriteRenderer);
            _spriteRenderer = spriteRenderer;

            _entity.TryGetFlippedData(out _flipped);
            
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
            _subscriptions = _flipped.Subscribe(OnFlip);
        }
        
        private void OnFlip(bool isFlipped)
        {
            _spriteRenderer.CurrentValue.flipX = isFlipped;
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
            _subscriptions = null;
        }
        
        #endregion
    }
}