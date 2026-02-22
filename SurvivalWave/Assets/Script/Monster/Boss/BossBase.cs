using UnityEngine;

public class BossBase : MonsterBase
{
    protected override void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);

        GameObject go = ItemPool.GetInstance().PopObject(ItemType.Box);
        go.transform.position = spawnPos;
        MonsterPool.GetInstance().ReturnObject(gameObject, type);
    }
}
