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
        private void OnTriggerEnter2D(Collider2D otherEntityCollider)
        {
        }
        
        private void OnTriggerStay2D(Collider2D otherEntityCollider)
        {
            otherEntityCollider.TryTakeDamageFrom(this);
        }

        private void OnTriggerExit2D(Collider2D otherEntityCollider)
        {
        }
    }
}