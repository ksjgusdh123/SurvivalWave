using UnityEngine;

public class PlayerProjectileSkillBase : PlayerSkillBase
{
    protected Transform ownerFirePosition;
    protected float cooldown;
    float timer;
    protected float damageRatio = 1f;

    protected PlayerProjectileSkillBase(int id, float cooldown)
        : base(id)
    {
        this.cooldown = cooldown;
        timer = 0f;
    }

    public override void OnEquip(GameObject ownerObj)
    {
        var player = ownerObj.GetComponent<Player>();
        if (null == player) return;

        ownerFirePosition = player.firePosition;
    }

    public override void TickEvent(float deltaTime)
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

    protected override bool TryFire()
    {
        return true;
    }

    public override void UpgradeStat()
    {
        var data = SkillDataManager.GetInstance().GetSkillData((SkillItemType)skillId);

        damageRatio = data.damageRatio + (data.increaseDamageRatio * (level - 1));
        cooldown = data.launchInterval - (data.decreaseLaunchInterval * (level - 1));
    }
}
