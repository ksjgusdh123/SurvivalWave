using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum ProjectileType
{ 
    RandomShot,
    Homing,
    Boomerang,
    Max
}


public class ProjectileSpawnManager : Singleton<ProjectileSpawnManager>
{
    [SerializeField] List<GameObject> ProjectilePrefab = new List<GameObject>();

    PlayerStat playerStat;

    protected override void Awake()
    {
        int cnt = (int)ProjectileType.Max;
        for (int i = 0; i < cnt; ++i)
        {
            ProjectilePrefab.Add(Resources.Load<GameObject>($"Prefab/Projectile/Player/{((ProjectileType)i).ToString()}"));
        }
    }

    public void RegisterPlayerStat(PlayerStat stat)
    {
        playerStat = stat;
    }
    public void SpawnRandomShot(Vector3 spawnPos, Vector3 dir, float speed, float maxDist, float dmgRatio)
    {
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject go = ProjectilePool.GetInstance().PopObject(ProjectileType.RandomShot);
        go.transform.SetPositionAndRotation(spawnPos, rot);
        //GameObject go = Instantiate(ProjectilePrefab[(int)ProjectileType.RandomShot], spawnPos, rot);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        var move = go.GetComponent<StraightMove>();
        if (null == pb || null == move) return;
        move.InitMove(dir, maxDist, spawnPos);
        pb.Init(move, speed, CalculateFinalDamage(dmgRatio));
        ParticleManager.GetInstance().SpawnParticle(pb.muzzle, ParticleType.RandomShotEffect, 1f);
    }

    public void SpawnHoming(Vector3 spawnPos, Transform target, float speed, float dmgRatio)
    {
        if (target == null) return;

        Vector3 dir = (target.position - spawnPos).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject go = ProjectilePool.GetInstance().PopObject(ProjectileType.Homing);
        go.transform.SetPositionAndRotation(spawnPos, rot);
        //GameObject go = Instantiate(ProjectilePrefab[(int)ProjectileType.Homing], spawnPos, rot);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        var move = go.GetComponent<HomingMove>();
        if (null == pb || null == move) return;
        move.InitMove(target);
        pb.Init(move, speed, CalculateFinalDamage(dmgRatio));

        ParticleManager.GetInstance().SpawnParticle(pb.muzzle, ParticleType.RocketEffect, 4f);
    }

    public void SpawnBoomerang(Vector3 spawnPos, Vector3 dir, float speed, float maxDist, float dmgRatio)
    {
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject go = ProjectilePool.GetInstance().PopObject(ProjectileType.Boomerang);
        go.transform.SetPositionAndRotation(spawnPos, rot);
        //GameObject go = Instantiate(ProjectilePrefab[(int)ProjectileType.Boomerang], spawnPos, rot);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        var move = go.GetComponent<BoomerangMove>();
        if (null == pb || null == move) return;
        move.InitMove(dir, maxDist);
        pb.Init(move, speed, CalculateFinalDamage(dmgRatio));
        pb.isPenetration = true;
        ParticleManager.GetInstance().SpawnParticle(pb.muzzle, ParticleType.BoomerangEffect, 1.5f);
    }

    float CalculateFinalDamage(float ratio)
    {
        return ratio * playerStat.attack;
    }
}
