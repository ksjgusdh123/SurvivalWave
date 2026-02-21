
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseObjectPool<T, TypeKey> : Singleton<T>
    where T : MonoBehaviour
    where TypeKey : Enum
{
    [Serializable]
    public class Entry
    {
        public TypeKey type;
        public GameObject go;
        public int initSize = 30;
    }

    [SerializeField] protected List<Entry> initDatas = new List<Entry>();
    protected bool isPrefab = true;
    protected Transform rootObject;
    protected Dictionary<TypeKey, Queue<GameObject>> pools = new Dictionary<TypeKey, Queue<GameObject>>();
    protected Dictionary<TypeKey, GameObject> prefabs = new Dictionary<TypeKey, GameObject>();
    protected Dictionary<TypeKey, int> initSizes = new Dictionary<TypeKey, int>();
    protected override void Awake()
    {
        pools.Clear();
        prefabs.Clear();

        Init();
        InitPoos();
    }
    protected abstract void Init();
    void InitPoos()
    {
        foreach (Entry data in initDatas)
        {
            prefabs[data.type] = data.go;
            initSizes[data.type] = data.initSize;
            Queue<GameObject> q = new Queue<GameObject>(data.initSize);
            InitPool(q, data);
        }
    }

    void InitPool(Queue<GameObject> pool, Entry data)
    {
        AllocatePool(pool, data.type);
        pools[data.type] = pool;
    }
    void AllocatePool(Queue<GameObject> pool, TypeKey type)
    {
        int size = initSizes[type];
        for (int i = 0; i < size; ++i)
        {
            GameObject go;
            if (isPrefab) go = Instantiate(prefabs[type], rootObject);
            else go = prefabs[type];
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public virtual GameObject PopObject(TypeKey type, Transform parent = null)
    {
        var q = pools[type];

        if (q.Count <= 0)
        {
            AllocatePool(q, type);
            Debug.Log("추가 스폰");
        }

        var go = q.Dequeue();
        if (parent) go.transform.SetParent(parent, false);
        go.SetActive(true);
        return go;
    }

    public virtual void ReturnObject(GameObject go, TypeKey type)
    {
        go.transform.SetParent(rootObject, false);
        go.SetActive(false);
        pools[type].Enqueue(go);
    }
}