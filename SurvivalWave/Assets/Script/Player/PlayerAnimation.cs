using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController PlayerController;

    static readonly int moveSpeed = Animator.StringToHash("Speed");
    static readonly int isJump = Animator.StringToHash("isJump");

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetFloat(moveSpeed, PlayerController.currentSpeed);
        animator.SetBool(isJump, PlayerController.isJump);
    }
}
