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
            Entry e = new Entry();
            e.type = (MonsterType)i;
            e.initSize = GetInitSize(e.type);
            e.go = Resources.Load<GameObject>($"Prefab/Monster/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
    protected override int GetInitSize(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Slime: return 500;
            case MonsterType.Turtle: return 500;
            case MonsterType.Boss: return 10;
            default: return 0;
        }
    }
}
