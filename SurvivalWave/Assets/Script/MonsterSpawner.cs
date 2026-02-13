using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

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
}
