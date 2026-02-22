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
            e.initSize = 300;
            e.type = (ParticleType)i;
            e.go = Resources.Load<GameObject>($"Prefab/Particle/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
}
