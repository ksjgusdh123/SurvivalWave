using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController PlayerController;

    static readonly int playerState = Animator.StringToHash("playerState");

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetInteger(playerState, (int)PlayerController.state);
    }
}
