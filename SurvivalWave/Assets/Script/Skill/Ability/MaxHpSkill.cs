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
        abilityRatio = level * skillData.increaseDamageRatio * 100f + 100f;
        GameManager.GetInstance().UpgradeAbility(StatType.MaxHp, abilityRatio);
    }
}
