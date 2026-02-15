using UnityEngine;

public class StrengthSkill : PlayerAbilitySkillBase
{
    float abilityRatio = 1f;
    public StrengthSkill()
       : base((int)SkillItemType.Strength)
    {
    }
    public override void UpgradeStat()
    {
        var data = SkillDataManager.GetInstance().GetSkillData((SkillItemType)skillId);
        abilityRatio = level * data.increaseDamageRatio + 1f;
        GameManager.GetInstance().UpgradeAbility(StatType.Attack, abilityRatio);
    }
}
