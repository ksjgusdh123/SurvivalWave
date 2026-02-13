using UnityEngine;

public abstract class PlayerSkillBase : IPlayerSkill
{
    protected Transform owner;
    protected float cooldown;
    float timer;

    protected PlayerSkillBase(float cooldown)
    {
        this.cooldown = cooldown;
        timer = 0f;
    }

    public void OnEquip(GameObject ownerObj)
    {
        owner = ownerObj.transform;
    }

    public void TickEvent(float deltaTime)
    {
        timer -= deltaTime;
        if (timer > 0f) return;

        if (TryFire())
        {
            timer = cooldown;
        }
        else
        {
            timer = 0.1f;
        }
    }

    protected abstract bool TryFire();
    public virtual void LevelUp() { }
}
