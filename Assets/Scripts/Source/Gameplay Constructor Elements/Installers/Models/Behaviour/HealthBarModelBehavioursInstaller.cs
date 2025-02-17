using System;
using GameplayConstructorElements.Behaviours.UI_Model;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class HealthBarModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddHealthBarBehaviourBehaviour(new HealthBarBehaviour(entity));
        }
    }
}