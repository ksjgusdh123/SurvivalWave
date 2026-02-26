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
        abilityRatio = level * skillData.increaseDamageRatio + 1f;
        GameManager.GetInstance().UpgradeAbility(StatType.Attack, abilityRatio);
    }
}
