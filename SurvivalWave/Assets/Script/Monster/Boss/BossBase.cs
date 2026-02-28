using UnityEngine;

public class BossBase : MonsterBase
{
    float rayStartHeight = 5f;
    float rayLength = 10f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);

        GameObject go = ItemPool.GetInstance().PopObject(ItemType.Box);

        Vector3 finalPos = Utility.FixPositionOnGround(spawnPos, groundMask, rayStartHeight, rayLength);
        go.transform.position = finalPos;

        MonsterPool.GetInstance().ReturnObject(gameObject, type);
    }
}
