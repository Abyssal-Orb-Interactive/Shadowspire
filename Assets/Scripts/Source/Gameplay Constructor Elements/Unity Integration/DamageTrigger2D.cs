using System;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class DamageTrigger2D : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D otherEntityGameObject)
        {
            Debug.Log(otherEntityGameObject.name);
            
            if(!gameObject.TryGetComponent<IEntity>(out var entity) || !entity.TryGetDamageData(out var damage)) return;
            if (!otherEntityGameObject.TryGetComponent<IEntity>(out var otherEntity)) return;
            
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