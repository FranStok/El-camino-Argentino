using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2;
    [SerializeField] float jumpHeight = 3f;

    InputAction moveAction;
    InputAction jumpAction;

    CharacterController characterController;

    float verticalSpeed;

    void Start()
    {
        verticalSpeed = 0;
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("move");
        jumpAction = InputSystem.actions.FindAction("jump");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;
        calculateVerticalSpeed();
        MovePlayer(moveValue);
    }

    void calculateVerticalSpeed()
    {
        if (characterController.isGrounded)
        {
            verticalSpeed = (jumpAction.triggered) ? Mathf.Sqrt(Physics.gravity.y * jumpHeight * -2) : -1;
            Debug.Log("SALTO"); 
            Debug.Log(verticalSpeed); 
            return;
        }
            Debug.Log("NO SALTO"); 
        verticalSpeed += Physics.gravity.y * Time.deltaTime; //La gravedad es negativa
    }

    void MovePlayer(Vector2 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x + Vector3.up * verticalSpeed;
        characterController.Move(move);
    }


}
