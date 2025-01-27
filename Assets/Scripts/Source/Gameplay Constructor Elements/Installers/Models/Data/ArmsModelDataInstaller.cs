using System;
using AtomicFramework.AtomicStructures;
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
        [SerializeField] private AtomicEvent<IEntity> _leftHandEquipActionEvent = new();

        public void InstallTo(IEntity entity)
        {
            entity.TryAddRightHandData(_rightArm);
            entity.TryAddLeftHandData(_leftArm);
            entity.TryAddRightHandEquipActionEventData(_rightHandEquipActionEvent);
            entity.TryAddLeftHandEquipActionEventData(_leftHandEquipActionEvent);
        }
    }
}