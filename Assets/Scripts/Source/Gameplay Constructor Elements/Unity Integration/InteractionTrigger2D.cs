using System;
using System.Runtime.CompilerServices;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;
using UseCases;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class InteractionTrigger2D : MonoBehaviour, IDisposable
    {
        private IDisposable _subscription = null;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!this.TryGetEntity(out var entity)) return;

            
            if(!other.TryGetEntity(out var otherEntity)) return;
            if(!otherEntity.TryGetInputHandlerData(out var inputHandler)) return;
            if(!inputHandler.CurrentValue.TryGetInteractionInputActionData(out var interactionInputAction)) return;
            
            _subscription = interactionInputAction.Subscribe( () => OnInteraction(entity, otherEntity));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OnInteraction(in IEntity entity, in IEntity otherEntity)
        {
            entity.TryInvokeInteractionWith(otherEntity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Dispose();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}