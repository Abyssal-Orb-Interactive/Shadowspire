using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class AIModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private List<IEntity> _enemiesInVision = new();
        [SerializeReference] private IEntityActionFabric<IEntity>[] _enemyInVisionEventActions = Array.Empty<IEntityActionFabric<IEntity>>();
        [SerializeField] private AtomicEvent<IEntity> _enemyInVision = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddEnemiesInVisionData(_enemiesInVision);
            _enemyInVision.SubscribeBy(_enemyInVisionEventActions, entity);
            entity.TryAddEnemyInVisionEventData(_enemyInVision);
        }
    }
}