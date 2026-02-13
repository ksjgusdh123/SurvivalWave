using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRadius = 200f;
    [SerializeField] float minSpawnRadius = 50f;
    [SerializeField] GameObject monsterPrefab;

    Transform player;

    float spawnInterval = 1f;
    float difficultyInterval = 10f;
    [SerializeField] float minSpawnInterval = 0.3f;

    float spawnTimer;
    float difficultyTimer;


    void Start()
    {
        player = Player.playerTransform;
        spawnTimer = spawnInterval;
        difficultyTimer = difficultyInterval;

        SpawnGroup();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        difficultyTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            Spawn();
            spawnTimer = spawnInterval;
        }

        if (difficultyTimer <= 0f)
        {
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.2f);
            difficultyTimer = difficultyInterval;
        }
    }

    public void Spawn()
    {
        if (null == monsterPrefab) return;

        float distance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector2 direction = Random.insideUnitCircle.normalized;

        Vector3 spawnPos = new Vector3(player.position.x + direction.x * distance, 0f, player.position.z + direction.y * distance);

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(spawnPos, out navHit, 5f, NavMesh.AllAreas))
        {
            spawnPos = navHit.position;
        }

        GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        if (null == monster) return;

        Collider col = monster.GetComponent<Collider>();
        if (null == col) return;

        float offset = monster.transform.position.y - col.bounds.min.y;
        monster.transform.position += Vector3.up * offset;
    }

    public void SpawnGroup()
    {
        if (null == monsterPrefab) return;

        Vector3 centerPos;

        if (!TryPickGroupCenterOnNavMesh(player.position, out centerPos)) return;

        for (int i = 0; i < 10; i++)
        {
            if (TryPickPointNearCenterOnNavMesh(centerPos, out Vector3 pos))
            {
                var m = Instantiate(monsterPrefab, pos, Quaternion.identity);

                var col = m.GetComponent<Collider>();
                if (col != null)
                {
                    float offset = m.transform.position.y - col.bounds.min.y;
                    m.transform.position += Vector3.up * offset;
                }
            }
        }
    }

    bool TryPickGroupCenterOnNavMesh(Vector3 playerPos, out Vector3 center)
    {
        Vector2 dir = Random.insideUnitCircle;
        float dist = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 guess = new Vector3(playerPos.x + dir.x * dist, playerPos.y, playerPos.z + dir.y * dist);

        if (NavMesh.SamplePosition(guess, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            center = hit.position;
            return true;
        }

        center = default;
        return false;
    }

    bool TryPickPointNearCenterOnNavMesh(Vector3 center, out Vector3 pos)
    {
        Vector2 radius = Random.insideUnitCircle * 4f;
        Vector3 spawnPos = new Vector3(center.x + radius.x, center.y, center.z + radius.y);
        int monsterLayerMask = LayerMask.GetMask("Monster");

        for (int i = 0; i < 5; ++i)
        {
            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                //if (!Physics.CheckSphere(hit.position, 1f, monsterLayerMask))
                {
                    pos = hit.position;
                    return true;
                }
            }
        }

        pos = default;
        return false;
    }
}
