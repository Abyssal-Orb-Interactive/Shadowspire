using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.Fractions_Model
{
    [Serializable]
    public sealed class FractionInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Fractions> _fraction = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddFractionData(_fraction);
        }
    }
}