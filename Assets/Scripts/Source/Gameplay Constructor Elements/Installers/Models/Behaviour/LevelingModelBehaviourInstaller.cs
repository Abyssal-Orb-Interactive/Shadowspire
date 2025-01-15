using System;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class LevelingModelBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddLevelingBehaviourBehaviour(new LevelingBehaviour(entity));
        }
    }
}