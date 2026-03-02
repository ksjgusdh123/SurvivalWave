using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    [SerializeField] float cellSize = 10f;
    [SerializeField] int activeRadiusCells = 2;
    [SerializeField] float cellUpdateInterval = 0.5f;
    [SerializeField] float farCellUpdateInterval = 3f;

    [SerializeField] int updateFarPerFrame = 200;
    [SerializeField] int updateCellPerFrame = 300;

    // 거리 상관없이 항상 업데이트 해야하는 오브젝트
    readonly List<ITickUpdate> always = new();
    // 거리검사하여 업데이트 해야하는 모든 오브젝트
    readonly List<ITickUpdate> checkAll = new();
    // 거리검사 결과 인접한 결과물 검사
    readonly List<ITickUpdate> checkActive = new();
    // 추가된 오브젝트 임시보관
    readonly List<ITickUpdate> pendingAdd = new();
    // 삭제할 오브젝트 임시보관
    readonly List<ITickUpdate> pendingRemove = new();
    // 모든 오브젝트 등록되어있는지 여부 및 업데이트를 위한 시간 경과 체크 타이머
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
            Tick(always[i], delta);
        }

        cellAcc += delta;
        if (cellAcc >= cellUpdateInterval)
        {
            if (null == updateCellCorouinte) updateCellCorouinte = StartCoroutine(UpdateCell());
        }

        grid.PickNearCellObject(player.position, activeRadiusCells, checkActive);


        int timeStamp = Time.frameCount; // 스탬프를 찍어 어느 프레임에서 업데이트 조건을 만족한지 표시
        for (int i = 0; i < checkActive.Count; ++i)
        {
            var e = checkActive[i];
            Tick(e, delta);
            e.checkStamp = timeStamp;

            if(e is IShadowCast s)
            {
                s.SetNearShadow(true);
            }
        }

        farCheckAcc += delta;
        if (farCheckAcc >= farCellUpdateInterval)
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

            if (e is IShadowCast s)
            {
                s.SetNearShadow(false);
            }

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
    void Tick(ITickUpdate e, float delta)
    {
        e.Tick(delta);
    }
    void TickWithInterval(ITickUpdate e, float delta)
    {
        // 멀리 있는 오브젝트 개별 업데이트 함수
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

        if (e.Policy == UpdatePolicy.Always)
        {
            RegisterDirect(e);
            return;
        }

        if (farTickUpdateCoroutine != null)
        {
            pendingAdd.Add(e);
            return;
        }

        RegisterDirect(e);
    }
    void RegisterDirect(ITickUpdate e)
    {
        acc[e] = 0f;

        if (UpdatePolicy.Always == e.Policy)
        {
            always.Add(e);
        }
        else
        {
            checkAll.Add(e);
            grid.Add(e);
        }
    }
    public void Unregister(ITickUpdate e)
    {
        if (e == null || pendingRemove.Contains(e))
            return;

        if (UpdatePolicy.Always == e.Policy)
        {
            UnregisterDirect(e);
        }
        else if (null != farTickUpdateCoroutine)
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

        if (UpdatePolicy.Always == e.Policy)
        {
            always.Remove(e);
        }
        else
        {
            checkAll.Remove(e);
            grid.Remove(e);
        }
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
