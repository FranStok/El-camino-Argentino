using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2;
    [SerializeField] float jumpSpeed = 1;

    InputAction moveAction;
    InputAction jumpAction;

    CharacterController CharacterController;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("move");
        jumpAction = InputSystem.actions.FindAction("jump");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;
        MovePlayer(moveValue);
    }

    void MovePlayer(Vector2 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;
        CharacterController.Move(move);
    }


}
