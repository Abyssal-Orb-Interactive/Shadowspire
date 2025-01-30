using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.Movement_Model
{
    [Serializable]
    public sealed class SpeedInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _speed = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddSpeedData(_speed);
        }
    }
}