using UnityEngine;

public class ProjectileSpawnManager : Singleton<ProjectileSpawnManager>
{
    [SerializeField] GameObject randomProjectilePrefab;
    [SerializeField] GameObject homingProjectilePrefab;

    protected override void Awake()
    {
        randomProjectilePrefab = Resources.Load<GameObject>("Prefab/Projectile/Player/RandomProjectile");
        homingProjectilePrefab = Resources.Load<GameObject>("Prefab/Projectile/Player/HomingProjectile");
    }

    public void SpawnRandomShot(Vector3 spawnPos, Vector3 dir, float speed, float maxDist)
    {
        GameObject go = Instantiate(randomProjectilePrefab, spawnPos, Quaternion.identity);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        if (null == pb) return;
        pb.Init(new RandomMove(dir, maxDist, spawnPos), speed);
    }

    public void SpawnHoming(Vector3 spawnPos, Transform target, float speed)
    {
        GameObject go = Instantiate(homingProjectilePrefab, spawnPos, Quaternion.identity);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        if (null == pb) return;
        pb.Init(new HomingMove(target), speed);
    }
}
