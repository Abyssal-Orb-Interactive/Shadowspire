using System;
using GameplayConstructorElements.Behaviours.MovementModel;
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
            entity.TryAddJumpBehaviourBehaviour(new JumpBehaviour(entity));
            entity.TryAddFreeFallingBehaviourBehaviour(new FreeFallingBehaviour(entity));
            entity.TryAddJumpHangingBehaviourBehaviour(new JumpHangingBehaviour(entity));
        }
    }
}