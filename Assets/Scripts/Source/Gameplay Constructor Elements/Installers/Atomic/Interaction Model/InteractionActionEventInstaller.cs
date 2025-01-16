using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.InteractionModel
{
    [Serializable]
    public sealed class InteractionActionEventInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicEvent<IEntity> _interactionActionEvent = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInteractionActionEventData(_interactionActionEvent);
        }
    }
}