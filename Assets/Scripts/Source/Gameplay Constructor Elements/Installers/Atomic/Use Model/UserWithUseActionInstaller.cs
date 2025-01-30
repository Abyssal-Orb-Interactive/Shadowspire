using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.UseModel.Behaviour
{
    [Serializable]
    public sealed class UserWithUseActionInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _user = new();
        [SerializeReference] private IEntityActionFabric[] _onUseActions = Array.Empty<IEntityActionFabric>();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddUserData(_user);
            
            _user.CurrentValue.TryGetInputHandlerData(out var inputHandler);
            inputHandler.CurrentValue.TryGetUseInputActionData(out var useInputAction);
            
            useInputAction.SubscribeBy(_onUseActions, entity);
        }
    }
}