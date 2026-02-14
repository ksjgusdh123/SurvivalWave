using UnityEngine;



public abstract class PlayerSkillBase : IPlayerSkill
{
    public int skillId { get; protected set; }
    public int level { get; protected set; } = 1;

    protected Transform ownerFirePosition;
    protected float cooldown;
    float timer;
    protected float damageRatio = 1f;

    protected PlayerSkillBase(int id, float cooldown)
    {
        this.cooldown = cooldown;
        skillId = id;
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
    public virtual void LevelUp() { ++level; UpgradeStat(); }
    public virtual void UpgradeStat()
    {
        var data = SkillDataManager.GetInstance().GetSkillData((ESkillType)skillId);

        damageRatio = data.damageRatio + (data.increaseDamageRatio * (level - 1));
        cooldown = data.launchInterval - (data.decreaseLaunchInterval * (level - 1));
    }
}
