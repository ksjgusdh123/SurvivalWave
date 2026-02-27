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
    protected override void Awake()
    {
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
        go.transform.position = spawnPos;
        //go.transform.localPosition = Vector3.zero;
        //go.transform.localRotation = Quaternion.identity;
    }
}
