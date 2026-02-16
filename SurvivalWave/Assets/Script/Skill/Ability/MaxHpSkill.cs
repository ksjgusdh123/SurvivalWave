using UnityEngine;

public class MaxHpSkill : PlayerAbilitySkillBase
{
    float abilityRatio = 100f;
    public MaxHpSkill()
     : base((int)SkillItemType.MaxHp)
    {
    }
    public override void UpgradeStat()
    {
        var data = SkillDataManager.GetInstance().GetSkillData((SkillItemType)skillId);
        abilityRatio = level * data.increaseDamageRatio * 100f + 100f;
        GameManager.GetInstance().UpgradeAbility(StatType.MaxHp, abilityRatio);
    }
}
