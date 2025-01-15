using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours
{
    [Serializable]
    public sealed class SpriteSetUpBehaviour : IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        private readonly IEntity _entity = null;
        
        private ReactiveLibraryFacade.IObservable<Sprite> _spriteObservable = null;
        private IAtomicValue<SpriteRenderer> _spriteRendererValue = null;
        
        private IDisposable _subscribe = null;

        public SpriteSetUpBehaviour()
        {
            _entity = new Entity();
        }

        public SpriteSetUpBehaviour(IEntity entity)
        {
            _entity = entity;
        }

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
            _subscribe = _spriteObservable.Subscribe(OnSpriteChange);
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
            _subscribe?.Dispose();
        }
    }
}