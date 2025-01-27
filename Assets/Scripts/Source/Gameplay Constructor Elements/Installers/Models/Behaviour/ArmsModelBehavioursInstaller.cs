using System;
using GameplayConstructorElements.Behaviours.InventoryModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class ArmsModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddEquipBehaviourBehaviour(new EquipBehaviour(entity));
        }
    }
}