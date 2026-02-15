using UnityEngine;

public class BoomerangSkill : PlayerProjectileSkillBase
{
    float spawnRadius;
    float projectileSpeed;
    float maxDist;

    public BoomerangSkill(float cooldown, float sr, float speed, float md)
    : base((int)SkillItemType.Boomerang, cooldown)
    {
        spawnRadius = sr;
        projectileSpeed = speed;
        maxDist = md;
    }

    protected override bool TryFire()
    {
        Vector3 pos = ownerFirePosition.position;
        Vector2 distance = Random.insideUnitCircle;

        Vector3 spawnPos = pos + new Vector3(distance.x, 0f, distance.y) * spawnRadius;
        Vector3 dir = (spawnPos - pos).normalized;

        ProjectileSpawnManager.GetInstance().SpawnBoomerang(spawnPos, dir, projectileSpeed, maxDist, 1f);
        return true;
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
