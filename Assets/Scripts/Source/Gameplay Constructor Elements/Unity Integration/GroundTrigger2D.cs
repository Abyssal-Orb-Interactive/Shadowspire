using System;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class GroundTrigger2D : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            
            if (!gameObject.TryGetEntity(out var entity)) return;
            if (!entity.TryGetIsGroundedData(out var isGrounded)) return;
            
            isGrounded.Value = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            
            if (!gameObject.TryGetEntity(out var entity)) return;
            if (!entity.TryGetIsGroundedData(out var isGrounded)) return;
            
            isGrounded.Value = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            
            if (!gameObject.TryGetEntity(out var entity)) return;
            if (!entity.TryGetIsGroundedData(out var isGrounded)) return;
            
            isGrounded.Value = false;
        }
    }
}