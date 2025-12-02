using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 7;
    [SerializeField] float jumpHeight = 1.2f;

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
            verticalSpeed = (jumpAction.WasPressedThisFrame()) ? Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) : -1;

        }
        verticalSpeed += Physics.gravity.y * Time.deltaTime; //La gravedad es negativa
    }

    void MovePlayer(Vector2 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x + Vector3.up * verticalSpeed*Time.deltaTime;
        characterController.Move(move);
    }


}
