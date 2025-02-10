using System;
using GameplayConstructorElements.EntityExtensions;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class PermanentDamageTrigger2D : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D otherEntityCollider)
        {
            otherEntityCollider.TryTakeDamageFrom(this);
        }
    }
}