using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    static readonly int moveSpeed = Animator.StringToHash("Speed");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateSpeed(float speed)
    {
        animator.SetFloat(moveSpeed, speed);
    }
}
