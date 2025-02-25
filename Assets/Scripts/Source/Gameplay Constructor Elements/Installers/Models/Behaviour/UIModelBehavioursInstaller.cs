using System;
using GameplayConstructorElements.Behaviours.UIModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using Source.Gameplay_Constructor_Elements.Behaviours.UI_Model;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class UIModelBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInventoryWindowTogglingBehaviourBehaviour(new InventoryWindowTogglingBehaviour(entity));
            entity.TryAddEscapeFromWindowBehaviourBehaviour(new EscapeFromWindowBehaviour(entity));
        }
    }
}