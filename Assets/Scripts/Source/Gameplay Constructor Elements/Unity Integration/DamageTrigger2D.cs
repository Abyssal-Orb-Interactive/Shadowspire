using System;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class DamageTrigger2D : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D otherEntityGameObject)
        {
            if (!gameObject.TryGetEntity(out var entity) || !entity.TryGetDamageData(out var damage)) return;
            if (!otherEntityGameObject.TryGetEntity(out var otherEntity)) return;
            
            otherEntity.TakeDamage(damage);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
        }

        private void OnTriggerExit2D(Collider2D other)
        {
        }
    }
}