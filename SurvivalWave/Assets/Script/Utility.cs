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
}
