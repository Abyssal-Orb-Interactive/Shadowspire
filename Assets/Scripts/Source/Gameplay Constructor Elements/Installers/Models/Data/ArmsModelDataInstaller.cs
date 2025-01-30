using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class ArmsModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _rightArm = new();
        [SerializeField] private AtomicReactiveProperty<IEntity> _leftArm = new();
        [SerializeField] private AtomicEvent<IEntity> _rightHandEquipActionEvent = new();
        [SerializeReference] private IEntityActionFabric<IEntity>[] _rightHandEquipActions = Array.Empty<IEntityActionFabric<IEntity>>();
        [SerializeField] private AtomicEvent<IEntity> _leftHandEquipActionEvent = new();
        [SerializeReference] private IEntityActionFabric<IEntity>[] _leftHandEquipActions = Array.Empty<IEntityActionFabric<IEntity>>();

        public void InstallTo(IEntity entity)
        {
            entity.TryAddRightHandData(_rightArm);
            entity.TryAddLeftHandData(_leftArm);
            
            _rightHandEquipActionEvent.SubscribeBy(_rightHandEquipActions, entity);
            _leftHandEquipActionEvent.SubscribeBy(_leftHandEquipActions, entity);
            
            entity.TryAddRightHandEquipActionEventData(_rightHandEquipActionEvent);
            entity.TryAddLeftHandEquipActionEventData(_leftHandEquipActionEvent);
        }
    }
}