using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool isJump { get; set; }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            isJump = true;
        }
    }


    public void ConsumeJump()
    {
        isJump = false;
    }
}
