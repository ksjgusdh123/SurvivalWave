using UnityEngine;

public static class Utility
{
    static public Transform GetNearestMonster(Vector3 center, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Monster"));

        Transform nearest = null;
        float minDist = float.MaxValue;

        foreach (Collider hit in hits)
        {
            float dist = (hit.transform.position - center).sqrMagnitude;

            if (dist < minDist)
            {
                if (hit.gameObject.GetComponent<Stat>().hp <= 0) continue;

                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

    static public Vector3 FixPositionOnGround(Vector3 spawnPos, LayerMask groundMask, float rayStartHeight = 5f, float rayLength = 10f)
    {
        Vector3 result = spawnPos;

        GameObject go = ItemPool.GetInstance().PopObject(ItemType.Box);
        Vector3 rayStart = spawnPos + Vector3.up * rayStartHeight;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, rayStartHeight + rayLength, groundMask, QueryTriggerInteraction.Ignore))
        {
            result = hit.point;
        }
        return result;
    }
    static public void MouseCursorOnOff(bool isOn)
    {
        Cursor.visible = isOn;
        Cursor.lockState = isOn ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}
