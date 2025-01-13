using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Visual.Data
{
    [Serializable]
    public sealed class SpriteRendererInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<SpriteRenderer> _spriteRenderer = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddSpriteRendererData(_spriteRenderer);
        }
    }
}