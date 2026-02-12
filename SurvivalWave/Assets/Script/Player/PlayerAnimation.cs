using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController PlayerController;

    static readonly int moveSpeed = Animator.StringToHash("Speed");

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetFloat(moveSpeed, PlayerController.CurrentSpeed);
    }
}
