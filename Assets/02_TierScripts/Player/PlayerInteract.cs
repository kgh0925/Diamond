using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    IInteractable interactable;
    [SerializeField] private InputAction Interact;

    private void OnEnable()
    {
        Interact.Enable();
    }
    private void OnDisable()
    {
        Interact.Disable();
    }
    private void Update()
    {
        if(interactable != null && Interact.WasPerformedThisFrame())
        {
            interactable.Interact(this);
            interactable = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable Interact = other.GetComponent<IInteractable>();
        if(Interact != null )
        {
            interactable = Interact;
            Debug.Log(Interact.GetDiscription());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable Interact = other.GetComponent<IInteractable>();
        if (Interact != null && interactable == Interact)
        {
            interactable = null;
            Debug.Log("아이템을 주울 수 있는 거리를 벗어나셨습니다.");
        }
    }
}
