using UnityEngine;

public class BossBase : MonsterBase
{
    protected override void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);

        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/Item/Box"), spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
