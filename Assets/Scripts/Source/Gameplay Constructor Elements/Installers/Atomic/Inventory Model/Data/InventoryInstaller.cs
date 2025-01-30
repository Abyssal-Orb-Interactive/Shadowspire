using System;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.InventoryModel.Data
{
    [Serializable]
    public sealed class InventoryInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private ObservableDictionary<string, int> _inventory = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInventoryData(_inventory);
        }
    }
}