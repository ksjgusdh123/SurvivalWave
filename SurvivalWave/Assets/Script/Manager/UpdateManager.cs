using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    [SerializeField] float cellSize = 5f;
    [SerializeField] int activeRadiusCells = 1;   
    [SerializeField] float cellUpdateInterval = 1f;
    [SerializeField] float farCellUpdateInterval = 1f;

    readonly List<ITickUpdate> always = new();
    readonly List<ITickUpdate> checkAll = new();
    readonly List<ITickUpdate> checkActive = new();
    readonly Dictionary<ITickUpdate, float> acc = new();
    HashSet<ITickUpdate> activeSet = new HashSet<ITickUpdate>();

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
            cellAcc = 0f;
            for (int i = 0; i < checkAll.Count; ++i)
            {
                grid.UpdateCell(checkAll[i]);
            }
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
            float t = farCheckAcc;
            farCheckAcc = 0f;

            for (int i = 0; i < checkAll.Count; ++i)
            {
                var e = checkAll[i];
                if (checkAll[i].checkStamp == timeStamp) continue;
                TickWithInterval(e, t);
            }
        }
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

        if (acc.ContainsKey(e)) return;

        acc[e] = 0f;

        if (UpdatePolicy.Always == e.Policy) always.Add(e);
        else checkAll.Add(e);
        grid.Add(e);
    }

    public void Unregister(ITickUpdate e)
    {
        if (e == null) return;

        acc.Remove(e);

        if (UpdatePolicy.Always == e.Policy) always.Remove(e);
        else checkAll.Remove(e);
        grid.Remove(e);
    }
}
