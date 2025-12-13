using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] float moveSpeed = 7;
    [SerializeField] float jumpHeight = 1.2f;

    InputAction moveAction;
    InputAction jumpAction;

    CharacterController characterController;
    
    [SerializeField]AudioSource walkSound;
    [FormerlySerializedAs("walkSound")] [SerializeField]AudioSource jumpSound;

    float verticalSpeed;
    
    
    void Start()
    {
        verticalSpeed = 0;
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("move");
        jumpAction = InputSystem.actions.FindAction("jump");
        
        RestoreTransform();
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
            verticalSpeed = 0;
            if (jumpAction.WasPressedThisFrame())
            {
                jumpSound.time = 0.17f;
                jumpSound.Play();
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            }
        }
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
    }

    void MovePlayer(Vector2 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y 
                     + transform.right * moveVector.x 
                     + Vector3.up * verticalSpeed * Time.deltaTime;

        if (moveVector == Vector2.zero || !characterController.isGrounded)
        {
            if (walkSound.isPlaying) walkSound.Stop();
        }
        else
        {
            if (!walkSound.isPlaying) walkSound.Play();
        }

        characterController.Move(move);
    }

    private void RestoreTransform()
    {
        (Vector3 position, Quaternion rotation)? data = PlayerPositionManager.Instance.getTransformPosition();
        if (data is not null)
        {
            transform.position = data.Value.position;
            transform.rotation = data.Value.rotation;
        }
    }

    private void OnDestroy()
    {
        PlayerPositionManager.Instance.SaveTransform(transform);
    }
}
