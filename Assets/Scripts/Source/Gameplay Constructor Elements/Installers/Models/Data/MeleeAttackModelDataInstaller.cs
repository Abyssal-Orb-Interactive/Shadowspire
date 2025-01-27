using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
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
	    public void InstallTo(IEntity entity)
	    {
		    entity.TryAddTargetsInDamageZoneData(_targets);
		    entity.TryAddMeleeAttackActionEventData(_meleeAttackActionEvent);
	    }
    }
}