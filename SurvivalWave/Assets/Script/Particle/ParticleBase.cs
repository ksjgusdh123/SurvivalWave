using UnityEngine;

public class ParticleBase : MonoBehaviour, IPoolEvent
{
    public ParticleType type;
    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void OnSpawnPool()
    {
        ps.Play(true);
    }
    public void OnReturnPool()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.Clear(true);
    }
    void OnParticleSystemStopped()
    {
        ParticlePool.GetInstance().ReturnObject(gameObject, type);
    }
}
