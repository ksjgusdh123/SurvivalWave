using UnityEngine;

public abstract class PlayerSkillBase : IPlayerSkill
{
    protected Transform ownerFirePosition;
    protected float cooldown;
    float timer;

    protected PlayerSkillBase(float cooldown)
    {
        this.cooldown = cooldown;
        timer = 0f;
    }

    public void OnEquip(GameObject ownerObj)
    {
        var player = ownerObj.GetComponent<Player>();
        if (null == player) return;

        ownerFirePosition = player.firePosition;
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
