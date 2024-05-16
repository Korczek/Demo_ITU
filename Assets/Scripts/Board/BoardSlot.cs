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

    // object on board (moveble unmovable) 
    
    
    
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
}