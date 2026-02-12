using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float CurrentSpeed { get; private set; }

    public float speed = 3.0f;

    PlayerInputHandler inputHandler;
    CharacterController characterController;

    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();        
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(inputHandler.MoveInput.x, 0, inputHandler.MoveInput.y);
        characterController.Move(move * speed * Time.deltaTime);

        CurrentSpeed = move.magnitude * speed;
    }
}
