using UnityEngine;
using UnityEngine.InputSystem.XR;


public class Stat : MonoBehaviour
{
    public float hp { get; set; } = 100f;
    public float maxHp { get; set; } = 100f;
    public float attack { get; set; } = 10f;
    public float speed { get; set; } = 3f;

    public void SetStat(float maxHp, float attack, float speed, bool isResetHp)
    {
        this.maxHp = maxHp;
        this.attack = attack;
        this.speed = speed;
        if(isResetHp) this.hp = maxHp;
    }

    public virtual bool TakeDamage(float dmg)
    {
        hp -= dmg;
        return true;
    }
}
