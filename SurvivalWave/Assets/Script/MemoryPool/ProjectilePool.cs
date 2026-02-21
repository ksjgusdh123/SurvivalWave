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
            Entry e = new BaseObjectPool<ProjectilePool, ProjectileType>.Entry();
            e.initSize = 300;
            e.type = (ProjectileType)i;
            e.go = Resources.Load<GameObject>($"Prefab/Projectile/Player/{e.type.ToString()}");
            initDatas.Add(e);
        }
    }
}
