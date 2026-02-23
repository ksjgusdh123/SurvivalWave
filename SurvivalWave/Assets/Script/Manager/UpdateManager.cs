using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    [SerializeField] float cellSize = 5f;
    [SerializeField] int activeRadiusCells = 1;   
    [SerializeField] float cellUpdateInterval = 1f;
    [SerializeField] float farCellUpdateInterval = 3f;

    [SerializeField] int updateFarPerFrame = 200;
    [SerializeField] int updateCellPerFrame = 200;

    readonly List<ITickUpdate> always = new();
    readonly List<ITickUpdate> checkAll = new();
    readonly List<ITickUpdate> checkActive = new();
    readonly List<ITickUpdate> pendingAdd = new();
    readonly List<ITickUpdate> pendingRemove = new();
    readonly Dictionary<ITickUpdate, float> acc = new();

    Coroutine farTickUpdateCoroutine;
    Coroutine updateCellCorouinte;
    Transform player;
    SpatialGrid grid;

    float cellAcc;
    float farCheckAcc;


    protected override void Awake()
    {
        player = Player.playerTransform;
        grid = new SpatialGrid(cellSize);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        for (int i = 0; i < always.Count; ++i)
        {
            TickWithInterval(always[i], delta);
        }

        cellAcc += delta;
        if (cellAcc >= cellUpdateInterval)
        {
            if(null == updateCellCorouinte) updateCellCorouinte = StartCoroutine(UpdateCell());
        }

        grid.PickNearCellObject(player.position, activeRadiusCells, checkActive);

        int timeStamp = Time.frameCount;
        for (int i = 0; i < checkActive.Count; ++i)
        {
            TickWithInterval(checkActive[i], delta);
            checkActive[i].checkStamp = timeStamp;
        }

        farCheckAcc += delta;
        if(farCheckAcc >= farCellUpdateInterval)
        {
            if (null != farTickUpdateCoroutine) return;

            float t = farCheckAcc;
            farCheckAcc = 0f;
            farTickUpdateCoroutine = StartCoroutine(FarTickUpdate(t, timeStamp));
        }
    }
    IEnumerator UpdateCell()
    {
        int cnt = 0;
        cellAcc = 0f;
        for (int i = 0; i < checkAll.Count; ++i)
        {
            grid.UpdateCell(checkAll[i]);
            cnt++;
            if (cnt >= updateCellPerFrame)
            {
                cnt = 0;
                yield return null;
            }
        }
        UpdatePenddingObject();
        updateCellCorouinte = null;
    }
    IEnumerator FarTickUpdate(float delta, int timeStamp)
    {
        int cnt = 0;

        for (int i = 0; i < checkAll.Count; i++)
        {
            var e = checkAll[i];
            if (e.checkStamp == timeStamp) continue;

            TickWithInterval(e, delta);

            cnt++;
            if (cnt >= updateFarPerFrame)
            {
                cnt = 0;
                yield return null; 
            }
        }
        farTickUpdateCoroutine = null;
    }
    void TickWithInterval(ITickUpdate e, float delta)
    {
        float interval = e.TickInterval;
        float t = acc[e];
        t += delta;

        if (t >= interval)
        {
            e.Tick(t);   
            t = 0f;
        }

        acc[e] = t;
    }
    public void Register(ITickUpdate e)
    {
        if (e == null) return;

        if (acc.ContainsKey(e) || pendingAdd.Contains(e)) return;

        if(null == farTickUpdateCoroutine)
        {
            pendingAdd.Add(e);
        }
        else
        {
            RegisterDirect(e);
        }
    }
    void RegisterDirect(ITickUpdate e)
    {
        acc[e] = 0f;

        if (UpdatePolicy.Always == e.Policy) always.Add(e);
        else checkAll.Add(e);
        grid.Add(e);
    }
    public void Unregister(ITickUpdate e)
    {
        if (e == null || pendingRemove.Contains(e)) return;

        if (null == farTickUpdateCoroutine)
        {
            pendingRemove.Add(e);
        }
        else
        {
            UnregisterDirect(e);
        }
    }
    void UnregisterDirect(ITickUpdate e)
    {
        acc.Remove(e);

        if (UpdatePolicy.Always == e.Policy) always.Remove(e);
        else checkAll.Remove(e);
        grid.Remove(e);
    }
    void UpdatePenddingObject()
    {
        foreach (var po in pendingRemove)
        {
            UnregisterDirect(po);
        }
        pendingRemove.Clear();

        foreach (var po in pendingAdd)
        {
            RegisterDirect(po);
        }
        pendingAdd.Clear();
    }
}
