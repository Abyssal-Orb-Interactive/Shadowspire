using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class FacingModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Facing> _currentFacing = new();
        [SerializeField] private AtomicReactiveProperty<Facing> _originFacing = new();
        [SerializeReference] private IEntityConditionFabric[] _conditionsFabrics = Array.Empty<IEntityConditionFabric>();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddCurrentFacingData(_currentFacing);
            entity.TryAddOriginFacingData(_originFacing);

            var canFace = new AtomicBoolMultiplication();
            canFace.AppendBy(_conditionsFabrics, entity);
            entity.TryAddCanFaceData(canFace);
        }
    }
}