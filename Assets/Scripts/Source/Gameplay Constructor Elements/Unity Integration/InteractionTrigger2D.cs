using System;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace GameplayConstructorElements.UnityIntegration
{
    [Serializable]
    public sealed class InteractionTrigger2D : MonoBehaviour, IDisposable
    {
        private IDisposable _subscription = null;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetEntity(out var entity);
            entity.TryGetInputHandlerData(out var inputHandler);
            inputHandler.CurrentValue.TryGetInteractionInputActionData(out var interactionInputAction);
            _subscription = interactionInputAction.Subscribe( () => OnInteraction(entity));
        }

        private void OnInteraction(IEntity otherEntity)
        {
            this.TryGetEntity(out var entity);
            entity.TryGetCoinsData(out var coins);
            otherEntity.TryGetCoinsData(out var wallet);
            
            wallet.Value += coins.CurrentValue;
            coins.Value = 0;
            
            Destroy(gameObject);
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