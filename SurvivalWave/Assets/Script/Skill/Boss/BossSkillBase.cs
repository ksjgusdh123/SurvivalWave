using UnityEngine;

public abstract class BossSkillBase : MonoBehaviour
{
    public float damage { get; protected set; } = 10f;
    public float cooltime { get; protected set; }

    public abstract void Casting(Transform target);
}
