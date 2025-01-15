using System;
using GameplayConstructorElements.Behaviours.InputHandlerModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class InputHandlerModelBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddMovementInputHandlingBehaviourBehaviour(new MovementInputHandlingBehaviour(entity));
        }
    }
}