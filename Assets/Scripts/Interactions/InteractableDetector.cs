using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{

    [SerializeField] float rayDistance = 3f;

    [SerializeField] TextMeshProUGUI interactionText;
    LayerMask layerMask;
    InputAction interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Interactable Objects");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        ShotCollisionRay();
    }

    void ShotCollisionRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rayDistance, layerMask))
        {
            IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
            interactionText.text = interactable.InteractMessage;
            if (interactAction.WasPressedThisFrame()) interactable.Interact();
            return;
        }
        interactionText.text = "";
    }
}
