using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Stat : MonoBehaviour
{
    public float hp { get; set; } = 100f;
    public float maxHp { get; set; } = 100f;

    public float attack { get; set; } = 10f;

    public virtual bool TakeDamage(float dmg)
    {
        hp -= dmg;
        return true;
    }
}
