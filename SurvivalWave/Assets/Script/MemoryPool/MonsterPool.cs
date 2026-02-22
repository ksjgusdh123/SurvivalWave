using UnityEngine;

public enum MonsterType
{
    Slime,
    Turtle,
    Boss,
    Max
}

public class MonsterPool : BaseObjectPool<MonsterPool, MonsterType>
{
    protected override void Init()
    {
        GameObject go = GameObject.Find("[MonsterPool]");
        if (null == go) go = new GameObject("[MonsterPool]");
        DontDestroyOnLoad(go);
        rootObject = go.transform;

        int size = (int)MonsterType.Max;
        for (int i = 0; i < size; ++i)
        {
            Entry e = new BaseObjectPool<MonsterPool, MonsterType>.Entry();
            e.initSize = 300;
            e.type = (MonsterType)i;
            e.go = Resources.Load<GameObject>($"Prefab/Monster/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
}
