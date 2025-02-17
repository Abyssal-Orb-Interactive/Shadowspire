using System;
using GameplayConstructorElements.Behaviours.AIModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Atomic.Global.AIModel.Behaviours
{
    [Serializable]
    public sealed class AimToNearestEnemyInVisionTriggerBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddAimToNearestEnemyInVisionTriggerBehaviourBehaviour(new AimToNearestEnemyInVisionTriggerBehaviour(entity));
        }
    }
}