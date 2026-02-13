using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float spawnInterval = 1f;
    Transform player;

    float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
        player = Player.playerTransform;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnHomingMoveProjectile();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnRandomMoveProjectile()
    {
        if (null == projectilePrefab) return;
        float radius = 5f;

        Vector3 playerPos = player.position;
        Vector2 dir2D = Random.insideUnitCircle;
        Vector3 spawnPos = new Vector3(playerPos.x + dir2D.x * radius, playerPos.y, playerPos.z + dir2D.y * radius);

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        if (null == projectile) return;

        ProjectileBase pb = projectile.GetComponent<ProjectileBase>();

        if (pb is PlayerRandomProjectile randomProj)
        {
            Vector3 direction = (spawnPos - playerPos).normalized;
            randomProj.InitRandomDirection(direction);
        }
    }

    void SpawnHomingMoveProjectile()
    {
        int mask = LayerMask.GetMask("Monster");
        Vector3 center = player.position;
        Collider[] hits = Physics.OverlapSphere(center, 50f, mask);

        Transform nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            float dist = (hit.transform.position - center).sqrMagnitude;

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        if (null == nearest) return;

        GameObject projectile = Instantiate(projectilePrefab, center, Quaternion.identity);
        if (null == projectile) return;
        ProjectileBase pb = projectile.GetComponent<ProjectileBase>();
        if(pb is PlayerHomingProjectile homingProj)
        {
            homingProj.InitHomingTarget(nearest);
        }
    }
}
