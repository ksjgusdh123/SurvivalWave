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
            SpawnRandomMoveProjectile();
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
        //if (null == pb) return;
        //RandomMove move = projectile.GetComponent<RandomMove>();
        //if (null == move) return;

        //move.InitRandomDirection(direction);
    }
}
