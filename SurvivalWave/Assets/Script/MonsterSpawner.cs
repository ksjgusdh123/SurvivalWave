using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType
{

}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRadius = 200f;
    [SerializeField] float minSpawnRadius = 50f;
    [SerializeField] GameObject[] monsterPrefab;
    [SerializeField] GameObject BossPrefab;

    Transform player;

    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float decreaseSpwanInterval = 0.05f;
    float beforeInterval;
    float difficultyInterval = 30f;
    [SerializeField] float minSpawnInterval = 0.3f;

    bool isFocusSpawn;
    float spawnTimer;
    float difficultyTimer;
    int groundMask;

    WaitForSeconds focusSpawnTimerHandle;

    void Start()
    {
        player = Player.playerTransform;
        spawnTimer = spawnInterval;
        difficultyTimer = difficultyInterval;

        focusSpawnTimerHandle = new WaitForSeconds(30f);
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        difficultyTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            int idx = Random.Range(0, 2);
            if (isFocusSpawn)
            {
                RandomSpawn(idx);
            }
            else
            {
                Spawn(monsterPrefab[idx]);
            }
            spawnTimer = spawnInterval;
        }

        if (difficultyTimer <= 0f)
        {
            if(isFocusSpawn) beforeInterval = Mathf.Max(minSpawnInterval, beforeInterval - decreaseSpwanInterval);
            else spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - decreaseSpwanInterval);
            difficultyTimer = difficultyInterval;
        }
    }

    void RandomSpawn(int idx)
    {
        float rand = Random.Range(0f, 10f);

        if (rand < 9f)
        {
            Spawn(monsterPrefab[idx]);
        }
        else
        {
            SpawnGroup(monsterPrefab[idx]);
        }
    }
    public void Spawn(GameObject prefab)
    {
        if (null == monsterPrefab) return;

        float distance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector2 direction = Random.insideUnitCircle.normalized;

        Vector3 spawnPos = new Vector3(player.position.x + direction.x * distance, player.position.y + 5f, player.position.z + direction.y * distance);

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(spawnPos, out navHit, 5f, NavMesh.AllAreas))
        {
            spawnPos = navHit.position;
        }

        if (Physics.Raycast(spawnPos + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 50f, groundMask))
        {
            spawnPos = hit.point;
        }


        GameObject monster = Instantiate(prefab, spawnPos, Quaternion.identity);
        if (null == monster) return;

        Collider col = monster.GetComponent<Collider>();
        if (null == col) return;

        float offset = monster.transform.position.y - col.bounds.min.y;
        monster.transform.position += Vector3.up * offset;
    }
    public void SpawnGroup(GameObject prefab)
    {
        if (null == monsterPrefab) return;

        Vector3 centerPos;

        if (!TryPickGroupCenterOnNavMesh(player.position, out centerPos)) return;

        for (int i = 0; i < 10; i++)
        {
            if (TryPickPointNearCenterOnNavMesh(centerPos, out Vector3 pos))
            {
                var m = Instantiate(prefab, pos, Quaternion.identity);

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

        if (NavMesh.SamplePosition(guess, out NavMeshHit hit, 100f, NavMesh.AllAreas))
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
            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 10f, NavMesh.AllAreas))
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

    void SetFocusSpawnState()
    {
        beforeInterval = spawnInterval;
        spawnInterval = minSpawnInterval;
        isFocusSpawn = true;
    }

    public void StartFocusSpawn()
    {
        SetFocusSpawnState();
        Spawn(BossPrefab);
        StartCoroutine(FocusSpawnMonster());
    }

    IEnumerator FocusSpawnMonster()
    {
        yield return focusSpawnTimerHandle;
        spawnInterval = beforeInterval;
        isFocusSpawn = false;
    }
}
