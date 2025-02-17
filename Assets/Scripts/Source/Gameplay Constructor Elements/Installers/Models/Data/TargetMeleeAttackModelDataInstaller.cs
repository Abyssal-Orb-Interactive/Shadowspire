using System;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class TargetMeleeAttackModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _aim = new();
        [SerializeField] private AtomicEvent<float, DamageType> _meleeAttackActionEvent = new();
        [SerializeReference] private IEntityActionFabric<float, DamageType>[] _meleeAttackActionEventActionsFabrics = Array.Empty<IEntityActionFabric<float, DamageType>>();
        [SerializeReference] private IEntityConditionFabric[] _canAttackConditionFabrics = Array.Empty<IEntityConditionFabric>();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddAimData(_aim);

            _meleeAttackActionEvent.SubscribeBy(_meleeAttackActionEventActionsFabrics, entity);
            entity.TryAddMeleeAttackActionEventData(_meleeAttackActionEvent);

            var canAttack = new AtomicBoolMultiplication();
            canAttack.AppendBy(_canAttackConditionFabrics, entity);
            entity.TryAddCanAttackData(canAttack);

        }
    }
}