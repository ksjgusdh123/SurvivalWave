using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator animator;
    Stat stat;

    static readonly int isDie = Animator.StringToHash("isDie");

    void Start()
    {
        animator = GetComponent<Animator>();
        stat = GetComponent<Stat>();
    }

    void Update()
    {
        animator.SetBool(isDie, stat.hp <= 0 ? true : false);
    }

}
