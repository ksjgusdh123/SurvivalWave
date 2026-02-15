using UnityEngine;

public class StrengthSkill : PlayerSkillBase
{
    float abilityRatio = 1f;
    public StrengthSkill()
       : base((int)SkillItemType.Strength, 0f)
    {
    }

    public override void OnEquip(GameObject ownerObj)
    {
        ApplyAbility();
    }

    protected override bool TryFire()
    {
        return true;
    }

    public override void LevelUp()
    {
        var data = SkillDataManager.GetInstance().GetSkillData((SkillItemType)skillId);
        abilityRatio = ++level * data.increaseDamageRatio + 1f;
        ApplyAbility();
    }

    public virtual void ApplyAbility()
    {
        // GameManager
        GameManager.GetInstance().UpgradeAbility(StatType.Attack, abilityRatio);
    }
}
