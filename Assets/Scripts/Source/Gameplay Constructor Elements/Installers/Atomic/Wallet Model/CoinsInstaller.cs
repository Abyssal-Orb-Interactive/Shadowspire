using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.WalletModel
{
    [Serializable]
    public sealed class CoinsInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<int> _coins = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddCoinsData(_coins);
        }
    }
}