using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours.InputHandlerModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using InputActions;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class InputHandlerModelInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<PlayerActions> _inputActions = new();
        [SerializeField] private AtomicEvent<float2> _movementInputAction = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddInputActionsData(_inputActions);
            entity.TryAddMovementInputActionData(_movementInputAction);
            
            entity.TryAddMovementInputHandlingBehaviourBehaviour(new MovementInputHandlingBehaviour(entity));
        }
    }
}