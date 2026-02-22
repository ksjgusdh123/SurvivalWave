using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    RandomShotEffect,
    RocketEffect,
    BoomerangEffect,
    RocketExplosion,
    EarthShatter,
    Max
}

public class ParticleManager : Singleton<ParticleManager>
{
    Dictionary<ParticleType, GameObject> particlePrefabDic = new Dictionary<ParticleType, GameObject>();
    protected override void Awake()
    {
        int cnt = (int)ParticleType.Max;
        for (int i = 0; i < cnt; ++i)
        {
            particlePrefabDic[(ParticleType)i] = Resources.Load<GameObject>($"Prefab/Particle/{((ParticleType)i).ToString()}");
        }
    }

    public void SpawnParticle(Transform muzzle, ParticleType type, float deleteTime)
    {
        GameObject go = ParticlePool.GetInstance().PopObject(type, muzzle);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }
    public void SpawnParticle(Vector3 spawnPos, ParticleType type, float deleteTime)
    {
        GameObject go = ParticlePool.GetInstance().PopObject(type);
    }
}
