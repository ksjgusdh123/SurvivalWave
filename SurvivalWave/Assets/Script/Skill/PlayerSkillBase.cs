using UnityEngine;



public abstract class PlayerSkillBase : IPlayerSkill
{
    public int skillId { get; protected set; }
    public int level { get; protected set; } = 1;

    //protected Transform ownerFirePosition;
    //protected float cooldown;
    //float timer;
    //protected float damageRatio = 1f;

    protected PlayerSkillBase(int id)
    {
        skillId = id;
    }

    public abstract void OnEquip(GameObject ownerObj);

    public abstract void TickEvent(float deltaTime);

    protected abstract bool TryFire();
    public virtual void LevelUp() { ++level; UpgradeStat(); }
    public abstract void UpgradeStat();
}
