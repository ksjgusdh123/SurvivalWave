using UnityEngine;

public abstract class BossSkillBase : MonoBehaviour
{
    public float damage { get; protected set; }
    public float cooltime { get; protected set; }

    public abstract void Casting(Transform target);
}
