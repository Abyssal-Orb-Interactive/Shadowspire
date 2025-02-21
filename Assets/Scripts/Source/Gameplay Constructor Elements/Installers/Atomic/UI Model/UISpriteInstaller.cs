using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.UIModel
{
    [Serializable]
    public sealed class UISpriteInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Sprite> _uiSprite = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddUISpriteData(_uiSprite);
        }
    }
}