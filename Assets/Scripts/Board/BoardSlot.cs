using UnityEngine;

public enum NeighborDirection
{
    Down,
    Left,
    Up,
    Right,
    Null
}

public enum SlotRole
{
    Start,
    Finish,
    Path,
    Obstacle,
    Null
}

public class BoardSlot : SpawnableObject
{
    public BoardSlot[] neighbors;
    public Vector2Int gridPos;

    private SlotDecor _decor;
    
    public SlotRole slotRole { get; private set; } = SlotRole.Path;
    
    public override void OnSpawn()
    {
        neighbors = new BoardSlot[4];
        gridPos = Vector2Int.zero;
    }

    public void SetRole(SlotRole role)
    {
        slotRole = role;
        // switch graphic 
        // reset connections witch neighbors 
    }

    private void SetGraphic()
    {
        if (_decor != null)
        {
            SpawnMgr.Instance.Despawn(_decor);
            _decor = null;
        }
        
        _decor = SpawnMgr.Instance.Spawn(slotRole.ToString(), Vector3.zero, Vector3.zero, transform)
            .GetComponent<SlotDecor>();
    }

    public void AnimSlotIn(float delay)
    {
        transform.localScale = Vector3.zero;
        transform.AnimScale(1, .45f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .Run();
    }

    public void OnInputDown()
    {
        transform.AnimScale(.9f, .25f)
            .Run(); // this is debug only for now 
        
        // push scale down and leave its there
        
        // start counter to display select bar 
        // if selected slot switch during preparation stop select bar from starting 
    }

    public void OnInputUp(bool isStillSelected)
    {
        transform.AnimScale(1f, .25f)
            .Run(); // debug only 
        // check if touched slot is still this same 
        // if true, then run this select bar 
        // else remove select bar 
    }
}