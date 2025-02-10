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
        [SerializeField] private AtomicEvent _interactionInputAction = new();
        [SerializeField] private AtomicEvent _useInputAction = new();
        [SerializeField] private AtomicEvent<float> _jumpInputAction = new();
        [SerializeField] private AtomicReactiveProperty<float> _jumpInputKeyMaxPressingTime = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInputActionsData(new AtomicReactiveProperty<PlayerActions>(new PlayerActions()));
            entity.TryAddMovementInputActionData(_movementInputAction);
            entity.TryAddInteractionInputActionData(_interactionInputAction);
            entity.TryAddUseInputActionData(_useInputAction);
            entity.TryAddJumpInputActionData(_jumpInputAction);
            entity.TryAddJumpInputKeyMaxPressingTimeData(_jumpInputKeyMaxPressingTime);
        }
    }
}