using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Visual.Data
{
    [Serializable]
    public sealed class FlippedInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<bool> _flipped = new AtomicReactiveProperty<bool>();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddFlippedData(_flipped);
        }
    }
}