using System.Runtime.CompilerServices;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.EntityExtensions
{
    public static class InteractionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryInvokeInteractionWith(this IEntity entity, IEntity otherEntity)
        {
            if(!entity.TryGetInteractionActionEventData(out var interactionActionEvent)) return false;
            interactionActionEvent.Invoke(otherEntity);
            return true;
        }
    }
}