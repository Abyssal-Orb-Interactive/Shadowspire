using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace Source.Gameplay_Constructor_Elements.Installers.Models.Data
{
    [Serializable]
    public sealed class DeathModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicEvent _deathEvent = new();
        [SerializeReference] private IEntityActionFabric[] _deathActionFabrics = default;
        
        public void InstallTo(IEntity entity)
        {
            _deathEvent.SubscribeBy(_deathActionFabrics, entity);
            entity.TryAddDeathEventData(_deathEvent);
        }
    }
}