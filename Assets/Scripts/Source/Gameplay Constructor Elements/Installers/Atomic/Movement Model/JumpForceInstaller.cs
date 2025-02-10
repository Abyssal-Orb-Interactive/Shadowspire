using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameplayConstructorElements.Installers.Atomic.Global.Movement_Model
{
    [Serializable]
    public sealed class JumpForceInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<float> _jumpHeight = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddJumpHeightData(_jumpHeight);
        }
    }
}