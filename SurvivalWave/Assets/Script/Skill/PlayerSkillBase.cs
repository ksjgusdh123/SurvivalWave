using UnityEngine;



public abstract class PlayerSkillBase : IPlayerSkill
{
    public int skillId { get; protected set; }
    public int level { get; protected set; } = 1;
    public SkillData skillData { get; protected set; } 
    //protected Transform ownerFirePosition;
    //protected float cooldown;
    //float timer;
    //protected float damageRatio = 1f;

    protected PlayerSkillBase(int id)
    {
        skillId = id;
        skillData = SkillDataManager.GetInstance().GetSkillData(id);
    }

    public abstract void OnEquip(GameObject ownerObj);

    public abstract void TickEvent(float deltaTime);

    protected abstract bool TryFire();
    public virtual void LevelUp() 
    {
        UIManager.GetInstance().UpdateSkillPanel(skillData, ++level);
        UpgradeStat();
    }
    public abstract void UpgradeStat();
}
