using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameplayConstructorElements.Installers.Models.Data
{
    [Serializable]
    public sealed class TorchModelDataInstaller : IEntityAtomicElementInstaller
    {
        [SerializeField] private AtomicReactiveProperty<Light2D> _light2D = new();
        [SerializeField] private AtomicReactiveProperty<float> _minFallowStrength = new();
        [SerializeField] private AtomicReactiveProperty<float> _currentFallowStrength = new();
        [SerializeField] private AtomicReactiveProperty<float> _targetFallowStrength = new();
        [SerializeField] private AtomicReactiveProperty<float> _fadeDuration = new();
        [SerializeField] private AtomicReactiveProperty<bool> _isActive = new();
        [SerializeField] private AtomicReactiveProperty<float> _ignitionDuration = new();
        [SerializeField] private AtomicReactiveProperty<float> _targetIgnitionFalloffStrength = new();
        [SerializeField] private AtomicReactiveProperty<float> _maxIgnitionFalloffStrength = new();
        public void InstallTo(IEntity entity)
        {
            entity.TryAddLight2DCompponentData(_light2D);
            entity.TryAddMinFalloffStrenghtData(_minFallowStrength);
            entity.TryAddCurrentFalloffStrenghtData(_currentFallowStrength);
            entity.TryAddTargetFalloffStrenghtData(_targetFallowStrength);
            entity.TryAddFadeDurationData(_fadeDuration);
            entity.TryAddIsActiveData(_isActive);
            entity.TryAddIgnitionDurationData(_ignitionDuration);
            entity.TryAddTargetIgnitionFalloffStrengthData(_targetIgnitionFalloffStrength);
            entity.TryAddMaxIgnitionFalloffStrengthData(_maxIgnitionFalloffStrength);
        }
    }
}