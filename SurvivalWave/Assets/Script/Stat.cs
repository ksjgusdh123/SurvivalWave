using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Stat : MonoBehaviour
{
    public float hp { get; private set; } = 100f;
    public float maxHp { get; private set; } = 100f;

    public float attack { get; private set; } = 10f;

    public virtual bool TakeDamage(float dmg)
    {
        hp -= dmg;
        return true;
    }
}
