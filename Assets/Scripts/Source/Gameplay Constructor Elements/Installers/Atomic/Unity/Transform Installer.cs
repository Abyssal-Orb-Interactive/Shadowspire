using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.Unity
{
    [Serializable]
    public sealed class TransformInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Transform> _transform = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddTransformData(_transform);
        }
    }
}