using System;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.DamageModel.Data
{
    [Serializable]
    public sealed class DamageModifiersInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private ObservableDictionary<DamageType, float> _damageModifiers = new();
        public void InstallTo(IEntity entity)
        {
            var damageModifiers = new ObservableDictionary<int, float>();

            foreach (var (type, modifier) in _damageModifiers)
            {
                damageModifiers[(int)type] = modifier;
            }
            
            entity.TryAddDamageModifiersData(damageModifiers);
        }
    }
}