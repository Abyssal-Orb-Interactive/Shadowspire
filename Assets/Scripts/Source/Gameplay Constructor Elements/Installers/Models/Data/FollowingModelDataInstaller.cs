using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class FollowingModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<IEntity> _target = new();
        [SerializeField] private AtomicReactiveProperty<float3> _distance = new();
        
        public void InstallTo(IEntity entity)
        {
            entity.TryAddTargetForFollowingData(_target);
            entity.TryAddFollowingDistanceData(_distance);
        }
    }
}