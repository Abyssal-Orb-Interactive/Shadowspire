using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class SpriteSetUpBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Variables
        
        private ReactiveLibraryFacade.IObservable<Sprite> _spriteObservable = null;
        private IAtomicValue<SpriteRenderer> _spriteRendererValue = null;
        
        #endregion
        
        #region Subscriptions
        
        private IDisposable _subscription = null;
        
        #endregion

        #region Constructors
        
        public SpriteSetUpBehaviour() {}

        public SpriteSetUpBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetSpriteData(out var spriteData);
            _spriteObservable = spriteData;
            
            _entity.TryGetSpriteRendererData(out var spriteRendererData);
            _spriteRendererValue = spriteRendererData;
            
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
            _subscription = _spriteObservable.Subscribe(OnSpriteChange);
        }

        private void OnSpriteChange(Sprite newSprite)
        {
            _spriteRendererValue.CurrentValue.sprite = newSprite;
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