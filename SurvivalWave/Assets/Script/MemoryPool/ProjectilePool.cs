using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ProjectilePool : BaseObjectPool<ProjectilePool, ProjectileType>
{
    protected override async Task Init()
    {
        GameObject go = GameObject.Find("[ProjectilePool]");
        if (null == go) go = new GameObject("[ProjectilePool]");
        DontDestroyOnLoad(go);
        rootObject = go.transform;

        int size = (int)ProjectileType.Max;
        for(int i = 0; i < size; ++i)
        {
            Entry e = new Entry();
            e.type = (ProjectileType)i;
            e.initSize = GetInitSize(e.type);
            e.go = await Addressables.LoadAssetAsync<GameObject>($"Prefab/Projectile/{e.type.ToString()}").Task;
            initDatas.Add(e);
        }
    }
    protected override int GetInitSize(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.Boomerang: return 2000;
            case ProjectileType.Homing: return 2000;
            case ProjectileType.RandomShot: return 6000;
            case ProjectileType.RedZone: return 20;
            default: return 0;
        }
    }
}
