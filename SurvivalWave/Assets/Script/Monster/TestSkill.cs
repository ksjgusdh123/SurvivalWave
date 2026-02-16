using System.Collections;
using UnityEngine;

public class TestSkill : BossSkillBase
{
    public GameObject redZonePrefab;   
    public float radius = 0.5f;
    public float warningTime = 0.8f;
    LayerMask targetMask;

    WaitForSeconds warningTimer;

    private void Start()
    {
        targetMask = LayerMask.NameToLayer("Player");
        warningTimer = new WaitForSeconds(warningTime);
    }

    public override void Casting(Transform target)
    {
        StartCoroutine(RedZoneCo(target));
    }
    IEnumerator RedZoneCo(Transform target)
    {
        Vector3 center = target.position;
        center.y = 0.01f;

        var warning = Instantiate(redZonePrefab, center, Quaternion.identity);
        warning.GetComponent<BlinkRedZone>().duration = warningTime; 

        yield return warningTimer;

        Collider[] hits = Physics.OverlapSphere(center, radius, targetMask);
        for (int i = 0; i < hits.Length; i++)
        {
            PlayerStat stat = hits[i].GetComponent<PlayerStat>();
            if (null != stat) stat.TakeDamage(10f);
        }

        Destroy(warning);
    }
}
