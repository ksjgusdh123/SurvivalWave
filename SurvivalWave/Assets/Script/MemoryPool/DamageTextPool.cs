using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum DamageTextEnum
{
    DamageText,
    Max
}

public class DamageTextPool : BaseObjectPool<DamageTextPool, DamageTextEnum>
{
    public override async Task Init(Action<float> action)
    {
        //GameObject go = GameObject.Find("[ItemPool]");
        //if (null == go) go = new GameObject("[ItemPool]");
        //DontDestroyOnLoad(go);
        //rootObject = go.transform;

        int size = (int)DamageTextEnum.Max;
        for (int i = 0; i < size; ++i)
        {
            Entry e = new Entry();
            e.type = (DamageTextEnum)i;
            e.initSize = GetInitSize(e.type);
            e.go = await Addressables.LoadAssetAsync<GameObject>($"Prefab/UI/DamageText").Task;
            initDatas.Add(e);

            float percent = (i + 1) / (float)size;
            action?.Invoke(percent);
        }
    }
    public void StartGameScene()
    {
        GameObject go = GameObject.Find("WorldSpaceCanvas");
        DontDestroyOnLoad(go);
        rootObject = go.transform;
        InstantiatePrefab();
    }

    protected override int GetInitSize(DamageTextEnum type)
    {
        return 500;
    }
}
