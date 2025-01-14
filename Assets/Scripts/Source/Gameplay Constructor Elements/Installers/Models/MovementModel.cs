using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class MovementModel : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Rigidbody2D> _rigidbody2D = new();
        [SerializeField] private AtomicReactiveProperty<float> _speed = new();
        [SerializeReference] private IEntityConditionFabric[] _canMoveConditionsFabtics = Array.Empty<IEntityConditionFabric>();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddRigidbody2DData(_rigidbody2D);
            entity.TryAddSpeedData(_speed);
            
            var canMoveCondition = new AtomicBoolMultiplication();
            canMoveCondition.AppendBy(_canMoveConditionsFabtics, entity);
            entity.TryAddCanMoveData(canMoveCondition);
            
            entity.TryAddMovementBehaviourBehaviour(new MovementBehaviour(entity));
        }
    }
}