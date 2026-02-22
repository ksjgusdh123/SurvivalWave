using UnityEngine;

public class ParticlePool : BaseObjectPool<ParticlePool, ParticleType>
{
    protected override void Init()
    {
        GameObject go = GameObject.Find("[ParticlePool]");
        if (null == go) go = new GameObject("[ParticlePool]");
        DontDestroyOnLoad(go);
        rootObject = go.transform;

        int size = (int)ParticleType.Max;
        for (int i = 0; i < size; ++i)
        {
            Entry e = new Entry();
            e.type = (ParticleType)i;
            e.initSize = GetInitSize(e.type);
            e.go = Resources.Load<GameObject>($"Prefab/Particle/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
    protected override int GetInitSize(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.BoomerangEffect: return 200;
            case ParticleType.RocketEffect: return 200;
            case ParticleType.RandomShotEffect: return 500;
            case ParticleType.EarthShatter: return 20;
            case ParticleType.RocketExplosion: return 0;
            default: return 0;
        }
    }
}
