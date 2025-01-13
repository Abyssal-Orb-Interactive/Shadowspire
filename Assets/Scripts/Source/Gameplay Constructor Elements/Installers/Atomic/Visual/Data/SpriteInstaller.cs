using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Visual.Data
{
    [Serializable]
    public sealed class SpriteInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Sprite> _sprite = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddSpriteData(_sprite);
        }
    }
}