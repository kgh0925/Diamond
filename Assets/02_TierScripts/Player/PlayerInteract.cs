using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    IInteractable interactable;
    private void OnTriggerEnter(Collider other)
    {
        IInteractable Interact = other.GetComponent<IInteractable>();
        if(Interact != null )
        {
            interactable = Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable Interact = other.GetComponent<IInteractable>();
        if (Interact != null && interactable == Interact)
        {
            interactable = null;
        }
    }
}
