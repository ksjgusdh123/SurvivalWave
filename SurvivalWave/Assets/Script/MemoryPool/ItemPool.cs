using UnityEngine;

public enum ItemType
{
    Exp,
    Box,
    HpPotion,
    Max
}

public class ItemPool : BaseObjectPool<ItemPool, ItemType>
{
    protected override void Init()
    {
        GameObject go = GameObject.Find("[ItemPool]");
        if (null == go) go = new GameObject("[ItemPool]");
        DontDestroyOnLoad(go);
        rootObject = go.transform;

        int size = (int)ItemType.Max;
        for (int i = 0; i < size; ++i)
        {
            Entry e = new Entry();
            e.initSize = 300;
            e.type = (ItemType)i;
            e.go = Resources.Load<GameObject>($"Prefab/Item/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
}
