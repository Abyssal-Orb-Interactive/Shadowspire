using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Atomic.Global.DamageModel.Data
{
    [Serializable]
    public sealed class CanTakeDamageInstaller : IEntityAtomicElementInstaller
    {
        [SerializeReference] private IEntityConditionFabric[] _entityConditionFabrics = Array.Empty<IEntityConditionFabric>();
        public void InstallTo(IEntity entity)
        {
            var canTakeDamage = new AtomicBoolMultiplication();
            canTakeDamage.AppendBy(_entityConditionFabrics, entity);

            entity.TryAddCanTakeDamageData(canTakeDamage);
        }
    }
}