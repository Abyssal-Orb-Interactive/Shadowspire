using System;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class TargetTrigger2D : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.TryGetEntity(out var otherEntity)) return;
            if(!this.TryGetEntity(out var entity)) return;
            if(!entity.TryGetTargetsInDamageZoneData(out var targets)) return;
            
            targets.Add(otherEntity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.TryGetEntity(out var otherEntity)) return;
            if(!this.TryGetEntity(out var entity)) return;
            if(!entity.TryGetTargetsInDamageZoneData(out var targets)) return;
            
            targets.Remove(otherEntity);
        }
    }
}