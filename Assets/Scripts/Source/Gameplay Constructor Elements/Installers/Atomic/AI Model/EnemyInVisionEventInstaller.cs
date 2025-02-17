using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.AIModel
{
    [Serializable]
    public sealed class EnemyInVisionEventInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private IEntityActionFabric<IEntity>[] _enemyInVisionEventActions = Array.Empty<IEntityActionFabric<IEntity>>();
        [SerializeField] private AtomicEvent<IEntity> _enemyInVision = new();
        
        public void InstallTo(IEntity entity)
        {
            _enemyInVision.SubscribeBy(_enemyInVisionEventActions, entity);
            entity.TryAddEnemyInVisionEventData(_enemyInVision);
        }
    }
}