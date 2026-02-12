using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
