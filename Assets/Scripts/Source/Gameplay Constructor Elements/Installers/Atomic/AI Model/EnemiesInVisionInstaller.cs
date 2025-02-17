using System;
using System.Collections.Generic;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.AIModel
{
    [Serializable]
    public sealed class EnemiesInVisionInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private List<IEntity> _enemiesInVision = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddEnemiesInVisionData(_enemiesInVision);
        }
    }
}