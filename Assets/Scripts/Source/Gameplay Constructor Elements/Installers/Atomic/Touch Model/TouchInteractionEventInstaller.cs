using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.TouchModel
{
    [Serializable]
    public sealed class TouchInteractionEventInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] IEntityActionFabric<IEntity>[] _actionsFabrics = Array.Empty<IEntityActionFabric<IEntity>>();
        [SerializeField] private AtomicEvent<IEntity> _touchInteractionEvent = new();
        
        public void InstallTo(IEntity entity)
        {
            _touchInteractionEvent.SubscribeBy(_actionsFabrics, entity);
            entity.TryAddTouchInteractionEventData(_touchInteractionEvent);
        }
    }
}