using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator animator;

    static readonly int isDie = Animator.StringToHash("isDie");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void NotifyIsDeath(bool b)
    {
        animator.SetBool(isDie, b);
    }
    
}
