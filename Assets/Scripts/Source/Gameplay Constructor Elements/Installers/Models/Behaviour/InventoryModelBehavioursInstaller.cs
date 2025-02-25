using System;
using GameplayConstructorElements.Behaviours.UIModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class InventoryModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInventoryDisplayingBehaviourBehaviour(new InventoryDisplayingBehaviour(entity));
        }
    }
}