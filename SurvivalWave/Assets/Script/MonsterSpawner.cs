using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] float mapScale = 1f;
    [SerializeField] GameObject monsterPrefab;

    GameObject player;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Spawn()
    {
        if (null == monsterPrefab) return;

        float scale = 5f * mapScale;

        Vector3 spawnPos = new Vector3(Random.Range(-scale, scale), 0f, Random.Range(-scale, scale));
        Vector3 rayStart = spawnPos + Vector3.up * 20f;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 50f))
        {
            spawnPos = hit.point;
        }

        GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        if (null == monster) return;

        Collider col = monster.GetComponent<Collider>();
        if (null == col) return;

        float offset = monster.transform.position.y - col.bounds.min.y;
        monster.transform.position += Vector3.up * offset;
    }
}
