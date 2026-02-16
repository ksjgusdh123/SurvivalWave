using UnityEngine;

public class WideShotSkill : PlayerProjectileSkillBase
{
    float spawnRadius;
    float projectileSpeed;
    float maxDist;
    int count;

    public WideShotSkill(float cooldown, float sr, float speed, float md, int cnt)
   : base((int)SkillItemType.WideShot, cooldown)
    {
        spawnRadius = sr;
        projectileSpeed = speed;
        maxDist = md;
        count = cnt;
    }

    protected override bool TryFire()
    {
        Vector3 pos = ownerFirePosition.position;
        float step = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = step * i * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)).normalized;
            Vector3 spawnPos = pos + dir * spawnRadius;
            ProjectileSpawnManager.GetInstance().SpawnRandomShot(spawnPos, dir, projectileSpeed, maxDist, 1f);
        }
        return true;
    }
}
