using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using AtomicFramework.AtomicStructures.Actions;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace Source.GameplayConstructorElements.Installers.Models.Data
{
	[Serializable]
    public sealed class MeleeAttackModelDataInstaller : IEntityAtomicElementInstaller
    {
	    [SerializeReference] private List<IEntity> _targets = new();
	    [SerializeField] private AtomicEvent<float, DamageType> _meleeAttackActionEvent = new();
	    [SerializeReference] private IEntityActionFabric<float, DamageType>[] _meleeAttackActionEventActionsFabrics = Array.Empty<IEntityActionFabric<float, DamageType>>();
	    [SerializeReference] private AtomicReactiveProperty<Collider2D> _targetsTriggerCollider = new();
	    public void InstallTo(IEntity entity)
	    {
		    entity.TryAddTargetsInDamageZoneData(_targets);

		    _meleeAttackActionEvent.SubscribeBy(_meleeAttackActionEventActionsFabrics, entity);
		    
		    entity.TryAddMeleeAttackActionEventData(_meleeAttackActionEvent);
		    
		    entity.TryAddTargetTrigger2DColliderData(_targetsTriggerCollider);
	    }
    }
}