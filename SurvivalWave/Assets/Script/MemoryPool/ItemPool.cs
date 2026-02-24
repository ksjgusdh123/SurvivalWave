using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum ItemType
{
    Exp,
    Box,
    HpPotion,
    Magnet,
    Max
}

public class ItemPool : BaseObjectPool<ItemPool, ItemType>
{
    protected override async Task Init()
    {
        GameObject go = GameObject.Find("[ItemPool]");
        if (null == go) go = new GameObject("[ItemPool]");
        DontDestroyOnLoad(go);
        rootObject = go.transform;

        int size = (int)ItemType.Max;
        for (int i = 0; i < size; ++i)
        {
            Entry e = new Entry();
            e.type = (ItemType)i;
            e.initSize = GetInitSize(e.type);
            e.go = await Addressables.LoadAssetAsync<GameObject>($"Prefab/Item/{e.type.ToString()}").Task;

            //e.go = Resources.Load<GameObject>($"Prefab/Item/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }

    protected override int GetInitSize(ItemType type)
    {
        switch (type)
        {
            case ItemType.Box: return 10;
            case ItemType.Exp: return 500;
            case ItemType.HpPotion: return 10;
            case ItemType.Magnet: return 10;
            default: return 0;
        }
    }
}
