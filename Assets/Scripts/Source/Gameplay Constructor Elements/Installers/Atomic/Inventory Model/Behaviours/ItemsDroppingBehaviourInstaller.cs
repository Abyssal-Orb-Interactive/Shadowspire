using System;
using GameplayConstructorElements.Behaviours.InventoryModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Atomic.Global.InventoryModel.Behaviours
{
    [Serializable]
    public sealed class ItemsDroppingBehaviourInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddItemsDroppingBehaviourBehaviour(new ItemsDroppingBehaviour(entity));
        }
    }
}