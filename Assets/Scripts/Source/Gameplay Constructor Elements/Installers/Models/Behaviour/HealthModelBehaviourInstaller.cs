using System;
using GameplayConstructorElements.Behaviours.Health_Model;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class HealthModelBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddHealthPercentCalculationBehaviourBehaviour(new HealthPercentCalculationBehaviour(entity));
        }
    }
}