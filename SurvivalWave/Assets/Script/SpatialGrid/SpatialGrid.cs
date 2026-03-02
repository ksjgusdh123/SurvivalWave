using System.Collections.Generic;
using UnityEngine;

public class SpatialGrid 
{
    readonly float cellSize;
    readonly Dictionary<Vector2Int, List<ITickUpdate>> cells = new();
    readonly Dictionary<ITickUpdate, Vector2Int> objectCellPosDic = new();

    public SpatialGrid(float cellSize)
    {
        this.cellSize = cellSize;
    }
    Vector2Int WorldToCell(Vector3 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.z / cellSize));
    }
    List<ITickUpdate> GetOrCreate(Vector2Int c)
    {
        if (!cells.TryGetValue(c, out var list)) cells[c] = list = new List<ITickUpdate>(32);
        return list;
    }
    public void Add(ITickUpdate e)
    {
        var c = WorldToCell(e.Position);
        GetOrCreate(c).Add(e);
        objectCellPosDic[e] = c;
    }
    public void Remove(ITickUpdate e)
    {
        if (!objectCellPosDic.TryGetValue(e, out var c)) return;
        if (cells.TryGetValue(c, out var list)) list.Remove(e);
        objectCellPosDic.Remove(e);
    }
    public void UpdateCell(ITickUpdate e)
    {
        if (!objectCellPosDic.TryGetValue(e, out var oldC)) return;
        var newC = WorldToCell(e.Position);
        if (newC == oldC) return;

        if (cells.TryGetValue(oldC, out var oldList)) oldList.Remove(e);
        GetOrCreate(newC).Add(e);
        objectCellPosDic[e] = newC;
    }
    public void PickNearCellObject(Vector3 playerPos, int nearRadius, List<ITickUpdate> updateList)
    {
        updateList.Clear();
        var cellCenterPos = WorldToCell(playerPos);

        // nearRadius - 검사하려는 주변 셀의 크기 절반
        for (int z = -nearRadius; z <= nearRadius; ++z)
        {
            for (int x = -nearRadius; x <= nearRadius; ++x)
            {
                var c = new Vector2Int(cellCenterPos.x + x, cellCenterPos.y + z);
                if (!cells.TryGetValue(c, out var list)) continue;
                updateList.AddRange(list);
            }
        }
    }
}
