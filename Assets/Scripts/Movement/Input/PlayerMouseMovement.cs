using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseMovement : MonoBehaviour
{
    [SerializeField] float lookSpeed = 1;
    [SerializeField] Transform cameraHolder;

    InputAction lookAction;

    float verticalRotation = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>() * Time.deltaTime * lookSpeed;
        RotatePlayer(lookValue);

    }

    void RotatePlayer(Vector2 rotationVector)
    {

        verticalRotation -= rotationVector.y; // se invierte el signo para que "mouse arriba" mire arriba
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * rotationVector.x);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

}
