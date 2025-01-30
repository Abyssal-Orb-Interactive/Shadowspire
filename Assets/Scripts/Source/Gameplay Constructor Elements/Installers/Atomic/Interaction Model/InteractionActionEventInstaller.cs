using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using Source.Gameplay_Constructor_Elements.Actions.Interaction_Event;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.InteractionModel
{
    [Serializable]
    public sealed class InteractionActionEventInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicEvent<IEntity> _interactionActionEvent = new();
        [SerializeReference] private IEntityActionFabric<IEntity>[] _interactionActions = Array.Empty<IEntityActionFabric<IEntity>>();
        public void InstallTo(IEntity entity)
        {
            _interactionActionEvent.SubscribeBy(_interactionActions, entity);
            
            entity.TryAddInteractionActionEventData(_interactionActionEvent);
        }
    }
}