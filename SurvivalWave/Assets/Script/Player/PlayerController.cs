using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed { get; private set; }
    public float yVelocity { get; private set; }
    public bool wasGrounded { get; private set; }

    public float gravity = -9.8f;
    public float rotationSpeed = 10.0f;
    public float speed = 3.0f;
    public float jumpHeight = 2f;

    PlayerInputHandler inputHandler;
    CharacterController characterController;

    Vector3 velocity;

    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();        
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y);
        bool isGrounded = characterController.isGrounded;

        if(Vector3.zero != move)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if (inputHandler.isJump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            inputHandler.ConsumeJump();
        }

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = move * speed + velocity;
        characterController.Move(finalMove * Time.deltaTime);

        currentSpeed = move.magnitude * speed;
        wasGrounded = isGrounded;
        yVelocity = velocity.y;
    }
}
