using System.Collections;
using UnityEngine;

public class Redzone : BossSkillBase
{
    public GameObject redZonePrefab;   
    public float radius = 0.5f;
    public float warningTime = 0.8f;
    LayerMask targetMask;

    WaitForSeconds warningTimer;

    private void Start()
    {
        targetMask = LayerMask.GetMask("Player");
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
            if (null != stat) stat.TakeDamage(damage);
        }

        ParticleManager.GetInstance().SpawnParticle(center, ParticleType.EarthShatter, 5f);

        Destroy(warning);
    }
}
