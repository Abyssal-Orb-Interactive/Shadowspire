using System;
using GameplayConstructorElements.Behaviours.DeathModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class DeathModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddDeathBehaviourBehaviour(new DeathBehaviour(entity));
        }
    }
}