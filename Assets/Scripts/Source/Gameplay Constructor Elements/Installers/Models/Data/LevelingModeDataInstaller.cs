using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Installers.Models
{
    [Serializable]
    public sealed class LevelingModeDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<int> _level = new();
        [SerializeField] private AtomicReactiveProperty<float> _targetXp = new();
        [SerializeField] private AtomicReactiveProperty<float> _xp = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddLevelData(_level);
            entity.TryAddXpTargetData(_targetXp);
            entity.TryAddXpData(_xp);
        }
    }
}