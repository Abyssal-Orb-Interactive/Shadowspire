using System;
using GameplayConstructorElements.Behaviours.Following_Model;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class FollowingModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddFollowTargetWithDistanceAndLerpBehaviourBehaviour(new FollowTargetWithDistanceAndLerpBehaviour(entity));
        }
    }
}