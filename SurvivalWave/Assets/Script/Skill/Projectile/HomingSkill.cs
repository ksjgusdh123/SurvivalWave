using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class HomingSkill : PlayerSkillBase
{
    float searchRadius;
    float projectileSpeed;

    public HomingSkill(float cooldown, float searchRadius, float projectileSpeed)
        : base(cooldown)
    {
        this.searchRadius = searchRadius;
        this.projectileSpeed = projectileSpeed;
    }

    protected override bool TryFire()
    {
        Transform target = Utility.GetNearestMonster(owner.position, searchRadius);
        if (target == null) return false;

        ProjectileSpawnManager.GetInstance().SpawnHoming(owner.position, target, projectileSpeed);
        return true;
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
