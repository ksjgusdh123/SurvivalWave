using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ParticlePool : BaseObjectPool<ParticlePool, ParticleType>
{
    public override async Task Init(Action<float> action)
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
            e.go = await Addressables.LoadAssetAsync<GameObject>($"Prefab/Particle/{e.type.ToString()}").Task;
            initDatas.Add(e);

            float percent = (i + 1) / (float)size;
            action?.Invoke(percent);

            await Task.Yield();
        }
    }
    protected override int GetInitSize(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.BoomerangEffect: return 2000;
            case ParticleType.RocketEffect: return 2000;
            case ParticleType.RandomShotEffect: return 5000;
            case ParticleType.EarthShatter: return 20;
            case ParticleType.RocketExplosion: return 0;
            default: return 0;
        }
    }
}
