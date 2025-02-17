using System;
using GameplayConstructorElements.Behaviours.AIModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class AIModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddAimToNearestEnemyInVisionTriggerBehaviourBehaviour(
                new AimToNearestEnemyInVisionTriggerBehaviour(entity));
            entity.TryAddAutoAttackAimBehaviourBehaviour(new AutoAttackAimBehaviour(entity));
        }
    }
}