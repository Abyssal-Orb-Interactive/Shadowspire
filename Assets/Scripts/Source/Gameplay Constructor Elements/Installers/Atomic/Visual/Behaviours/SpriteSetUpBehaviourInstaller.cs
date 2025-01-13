using System;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Atomic.Global.Visual.Behaviours
{
    [Serializable]
    public sealed class SpriteSetUpBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddSpriteSetUpBehaviourBehaviour(new SpriteSetUpBehaviour(entity));
        }
    }
}