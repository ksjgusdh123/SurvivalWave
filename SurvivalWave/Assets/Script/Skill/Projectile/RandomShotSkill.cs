using UnityEngine;

public class RandomShotSkill : PlayerProjectileSkillBase
{
    float spawnRadius;
    float projectileSpeed;
    float maxDist;

    public RandomShotSkill(float cooldown, float sr, float speed, float md)
        : base((int)SkillItemType.RandomShot ,cooldown)
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

        ProjectileSpawnManager.GetInstance().SpawnRandomShot(spawnPos, dir, projectileSpeed, maxDist, 1f);
        return true;
    }
}
