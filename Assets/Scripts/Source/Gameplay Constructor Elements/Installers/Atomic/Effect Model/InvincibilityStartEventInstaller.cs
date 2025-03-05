using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.EffectModel
{
    [Serializable]
    public sealed class InvincibilityStartEventInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private IEntityActionFabric[] _actionFabrics = Array.Empty<IEntityActionFabric>();
        [SerializeField] private AtomicEvent _invincibilityStartEvent = new();
        public void InstallTo(IEntity entity)
        {
            _invincibilityStartEvent.SubscribeBy(_actionFabrics, entity);
            
            entity.TryAddInvincibilityStartEventData(_invincibilityStartEvent);
        }
    }
}