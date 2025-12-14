using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InteractableDetector : MonoBehaviour
{

    [SerializeField] float rayDistance = 3f;

    [SerializeField] TextMeshProUGUI interactionTextElement;
    LayerMask InteractableObjectLayer;
    InputAction interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractableObjectLayer = LayerMask.GetMask("Interactable Objects");
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
        if (Physics.Raycast(ray, out raycastHit, rayDistance, InteractableObjectLayer))
        {
            IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
            
            interactionTextElement.text = interactable.InteractMessage;
            if (interactAction.WasPressedThisFrame()) interactable.Interact();
            
            
            return;
        }
        interactionTextElement.text = "";
    }
}
