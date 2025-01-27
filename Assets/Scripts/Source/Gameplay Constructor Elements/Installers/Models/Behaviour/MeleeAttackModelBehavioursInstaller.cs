using System;
using GameplayConstructorElements.Behaviours.AttackModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class MeleeAttackModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddSplashMeleeAttackBehaviourBehaviour(new SplashMeleeAttackBehaviour(entity));
        }
    }
}