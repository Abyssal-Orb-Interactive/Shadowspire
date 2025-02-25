using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayConstructorElements.Behaviours.UI_Model
{
    [Serializable]
    public sealed class ItemsIconDisplayingBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Varibles

        private IReadonlyAtomicReactiveProperty<IEntity> _displayingEntity = null;
        private IAtomicValue<Image> _icon  = null;
        private IReadonlyAtomicReactiveProperty<Sprite> _iconSprite  = null;
        private IAtomicValue<GameObject> _iconGameObject;

        #endregion

        #region Subscriptions

        private IDisposable _iconSubscription = null;
        private IDisposable _entitySubscription = null;

        #endregion

        #region Constructors

        public ItemsIconDisplayingBehaviour() {}
        public ItemsIconDisplayingBehaviour(IEntity entity) : base(entity) {}

        #endregion
        
        public void Init()
        {
            _entity.TryGetDisplayingEntityData(out var displayingEntity);
            _displayingEntity = displayingEntity;

            _entity.TryGetIconData(out var icon);
            _icon = icon;
            
            _entity.TryGetIconGameObjectData(out var iconGameObject);
            _iconGameObject = iconGameObject;
            
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
            _entitySubscription = _displayingEntity.Subscribe(OnDisplayingEntityChanged);
        }

        private void OnDisplayingEntityChanged(IEntity newEntity)
        {
            _iconSubscription?.Dispose();
            _iconSubscription = null;
            
            if (newEntity == null)
            {
                _iconGameObject.CurrentValue.SetActive(false);
                return;
            }
            
            if (!newEntity.TryGetUISpriteData(out var uiSprite))
            {
                Debug.Log("Here");
                _iconGameObject.CurrentValue.SetActive(false);
                return;
            }
            
            
            if(!_iconGameObject.CurrentValue.activeSelf) _iconGameObject.CurrentValue.SetActive(true);
            
            _iconSprite = uiSprite;
            
            _iconSubscription = _iconSprite.Subscribe(OnIconChanged);
        }

        private void OnIconChanged(Sprite newIcon)
        {
            _icon.CurrentValue.sprite = newIcon;
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
            _iconSubscription?.Dispose();
            _iconSubscription = null;
            _entitySubscription?.Dispose();
            _entitySubscription = null;
        }
    }
}