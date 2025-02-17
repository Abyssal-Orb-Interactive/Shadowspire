using System;
using System.Collections.Generic;
using System.Linq;
using AtomicFramework.AtomicStructures;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.Behaviours.AIModel
{
    [Serializable]
    public sealed class AimToNearestEnemyInVisionTriggerBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour , IFrameRunBehaviour, IDisposable
    {
        private IEntity _nearestEnemy = null;
        private float _nearestDistance = float.MaxValue;

        #region Cache Variables
        
        private AtomicReactiveProperty<IEntity> _aim = new();
        private IReadOnlyList<IEntity> _enemies = null;
        private IAtomicValue<Transform> _transform = null;
        private IAtomicAction<IEntity> _enemyInVision = null;

        #endregion
        
        #region Constructors
        
        public AimToNearestEnemyInVisionTriggerBehaviour() {}
        public AimToNearestEnemyInVisionTriggerBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        public void Init()
        {
            _entity.TryGetEnemiesInVisionData(out var enemies);
            _enemies = enemies;
            
            _entity.TryGetTransformData(out var transform);
            _transform = transform;
            
            _entity.TryGetEnemyInVisionEventData(out var enemyInVision);
            _enemyInVision = enemyInVision;
            
            _entity.TryGetAimData(out var aim);
            _aim = aim;
            
            OnInit();
        }
        
        public void OnInit()
        {
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _aim.Subscribe(OnAimChanged);
        }

        private void OnAimChanged(IEntity newAim)
        {
            _enemyInVision.Invoke(newAim);
        }

        public void OnFrameRun()
        {
            
            var count = _enemies.Count;

            for (var i = 0; i < count; i++)
            {
                if(!_enemies[i].TryGetTransformData(out var transform)) continue;

                var distance = Vector2.Distance(_transform.CurrentValue.position, transform.CurrentValue.position);

                if (distance > _nearestDistance) continue;
                
                _nearestDistance = distance;
                _nearestEnemy = _enemies[i];
            }
            
            _aim.Value = _nearestEnemy;
            
            if (!_enemies.Contains(_aim.CurrentValue)) _aim.Value = null;
        }

        public void Sleep()
        {
            OnSleep();
        }

        public void OnSleep()
        {
            Dispose();
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _aim?.Dispose();
        }
    }
}