using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class InputHandlerModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicEvent<float2> _movementInputAction = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInputActionsData(new AtomicReactiveProperty<PlayerActions>(new PlayerActions()));
            entity.TryAddMovementInputActionData(_movementInputAction);
        }
    }
}