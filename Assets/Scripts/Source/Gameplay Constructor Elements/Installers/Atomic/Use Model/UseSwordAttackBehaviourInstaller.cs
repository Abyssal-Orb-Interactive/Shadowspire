using System;
using GameplayConstructorElements.Behaviours.Use_Model;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Atomic.Global.UseModel
{
    [Serializable]
    public sealed class UseSwordAttackBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddUseSwordAttackBehaviourBehaviour(new UseSwordAttackBehaviour(entity));
        }
    }
}