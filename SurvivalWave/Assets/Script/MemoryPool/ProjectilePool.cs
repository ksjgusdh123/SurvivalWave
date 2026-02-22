using Unity.VisualScripting;
using UnityEngine;

public class ProjectilePool : BaseObjectPool<ProjectilePool, ProjectileType>
{
    protected override void Init()
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
            e.go = Resources.Load<GameObject>($"Prefab/Projectile/Player/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
    protected override int GetInitSize(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.Boomerang: return 200;
            case ProjectileType.Homing: return 200;
            case ProjectileType.RandomShot: return 600;
            default: return 0;
        }
    }
}
