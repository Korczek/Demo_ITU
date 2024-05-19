using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NeighborDirection
{
    Down,
    Left,
    Up,
    Right,
    Null
}
public class BoardSlot : SpawnableObject
{
    public BoardSlot[] neighbors;
    public Vector2Int gridPos;

    public Path myPathElement;
    public bool IsFinish { get; private set; }
    
    public override void OnSpawn()
    {
        neighbors = new BoardSlot[4];
        gridPos = Vector2Int.zero;
    }

    public void AnimSlotIn(float delay)
    {
        transform.localScale = Vector3.zero;
        transform.AnimScale(1, .45f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .Run();
    }

    public bool DoIHavePathToFinish(HashSet<BoardSlot> visited)
    {
        if (visited.Contains(this))
            return false;
        
        visited.Add(this);

        if (IsFinish)
            return true;

        foreach (var n in neighbors.Where(n => n != null))
        {
            if (!n.DoIHavePathToFinish(visited))
                continue;
            
            return true;
        }

        return false;
    }
}