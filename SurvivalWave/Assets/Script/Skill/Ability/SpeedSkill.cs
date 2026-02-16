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
        var data = SkillDataManager.GetInstance().GetSkillData((SkillItemType)skillId);
        abilityRatio = level * data.increaseDamageRatio * 3f + 3f;
        GameManager.GetInstance().UpgradeAbility(StatType.Speed, abilityRatio);
    }
}
