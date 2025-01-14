using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.InputModel
{
    [Serializable]
    public sealed class InputHandlerInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _inputHandler = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInputHandlerData(_inputHandler);
        }
    }
}