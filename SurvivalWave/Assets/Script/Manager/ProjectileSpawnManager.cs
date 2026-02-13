using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{ 
    RandomShot,
    Homing,
    Max
}


public class ProjectileSpawnManager : Singleton<ProjectileSpawnManager>
{
    [SerializeField] List<GameObject> ProjectilePrefab = new List<GameObject>();

    protected override void Awake()
    {
        int cnt = (int)SkillType.Max;
        for (int i = 0; i < cnt; ++i)
        {
            ProjectilePrefab.Add(Resources.Load<GameObject>($"Prefab/Projectile/Player/{((SkillType)i).ToString()}"));
        }
    }

    public void SpawnRandomShot(Vector3 spawnPos, Vector3 dir, float speed, float maxDist)
    {
        GameObject go = Instantiate(ProjectilePrefab[(int)SkillType.RandomShot], spawnPos, Quaternion.identity);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        if (null == pb) return;
        pb.Init(new RandomMove(dir, maxDist, spawnPos), speed);
    }

    public void SpawnHoming(Vector3 spawnPos, Transform target, float speed)
    {
        GameObject go = Instantiate(ProjectilePrefab[(int)SkillType.Homing], spawnPos, Quaternion.identity);
        ProjectileBase pb = go.GetComponent<ProjectileBase>();
        if (null == pb) return;
        pb.Init(new HomingMove(target), speed);
    }
}
