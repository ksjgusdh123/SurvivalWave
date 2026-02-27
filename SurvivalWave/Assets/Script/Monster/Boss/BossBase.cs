using UnityEngine;

public class BossBase : MonsterBase
{
    float rayStartHeight = 5f;
    float rayLength = 10f;
    LayerMask groundMask;
    private void Awake()
    {
        groundMask = LayerMask.GetMask("Ground");
    }
    protected override void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);

        GameObject go = ItemPool.GetInstance().PopObject(ItemType.Box);
        Vector3 rayStart = spawnPos + Vector3.up * rayStartHeight;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, rayStartHeight + rayLength, groundMask, QueryTriggerInteraction.Ignore))
        {
            go.transform.position = hit.point;
        }
        else
        {
            go.transform.position = spawnPos;
        }


        MonsterPool.GetInstance().ReturnObject(gameObject, type);
    }
}
