using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    RandomShotEffect,
    RocketEffect,
    BoomerangEffect,
    RocketExplosion,
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
        GameObject go = Instantiate(particlePrefabDic[type], muzzle.position, Quaternion.identity, muzzle);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        StartCoroutine(ParticleLifeUpdate(go, deleteTime));
    }

    IEnumerator ParticleLifeUpdate(GameObject go, float deltaTime)
    {
        yield return new WaitForSeconds(deltaTime);
        if (null == go) yield break;
        Destroy(go);
    }
}
