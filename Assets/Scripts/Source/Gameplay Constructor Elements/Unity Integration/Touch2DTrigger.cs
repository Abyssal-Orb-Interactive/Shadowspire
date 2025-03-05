using GameplayConstructorElements.EntityExtensions;
using UnityEngine;

public class Touch2DTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        this.TryInvokeTouchInteractionEventWith(other);
    }
}
