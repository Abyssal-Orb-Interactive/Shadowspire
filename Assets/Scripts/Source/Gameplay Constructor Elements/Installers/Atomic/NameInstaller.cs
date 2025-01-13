using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global
{
    [Serializable]
    public sealed class NameInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<string> _name = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddNameData(_name);
        }
    }
}