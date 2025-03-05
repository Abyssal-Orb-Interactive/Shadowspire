using System;
using System.Collections.Generic;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using ObservableCollections;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class VisionTrigger2D : MonoBehaviour
    {

       private IEntity _entity = null;
       private List<IEntity> _enemiesInVision = new();
       private IAtomicValue<Fractions> _fraction = null;

       private void Start()
       {
           gameObject.transform.parent.TryGetEntity(out _entity);
           
           if (_entity == null) return;
           
           _entity.TryGetFractionData(out var fraction);
           _fraction = fraction;
           
           _entity.TryGetEnemiesInVisionData(out var enemiesInVision);
           _enemiesInVision = enemiesInVision;
       }

       private void OnTriggerEnter2D(Collider2D other)
        {
            if(_entity == null || _fraction == null) return;
            
            if (!other.TryGetEntity(out var otherEntity)) return;
            if (!otherEntity.TryGetFractionData(out var otherFraction))  return;
            if (_fraction.CurrentValue == otherFraction.CurrentValue) return;
            
            _enemiesInVision.Add(otherEntity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(_entity == null || _fraction == null) return;
            
            if (!other.TryGetEntity(out var otherEntity)) return;
            if (!otherEntity.TryGetFractionData(out var otherFraction))  return;
            if (_fraction.CurrentValue == otherFraction.CurrentValue) return;
            
            
            _enemiesInVision.Remove(otherEntity);
        }
    }
}