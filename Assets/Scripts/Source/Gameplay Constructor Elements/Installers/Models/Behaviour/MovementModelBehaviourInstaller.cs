using System;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class MovementModelBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddMovementBehaviourBehaviour(new MovementBehaviour(entity));
        }
    }
}