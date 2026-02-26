using UnityEngine;

public class SpeedSkill : PlayerAbilitySkillBase
{
    float abilityRatio = 3f;
    public SpeedSkill()
     : base((int)SkillItemType.Speed)
    {
    }
    public override void UpgradeStat()
    {
        abilityRatio = level * skillData.increaseDamageRatio * 3f + 3f;
        GameManager.GetInstance().UpgradeAbility(StatType.Speed, abilityRatio);
    }
}
