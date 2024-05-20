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
    public MeshRenderer meshRenderer;
    
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

    public void Initialize(float delay, SlotRole role)
    {
        slotRole = role;
        meshRenderer.sharedMaterial = Const.Materials.slotsMaterials[(int)role];
        transform.localScale = Vector3.zero;
        transform.AnimScale(1, .45f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .Run();
        
        if (role == SlotRole.Start)
            Mgr.Instance.SetStartPoint(this);
        
        if (role == SlotRole.Finish)
            Mgr.Instance.SetFinishPoint(this);
    }

    public void ShowAsPath(float delay)
    {
        transform.AnimScale(.8f, .2f)
            .OnComplete(() =>
            {
                if (slotRole == SlotRole.Path)
                    meshRenderer.sharedMaterial = Const.Materials.GetStepMaterial;
                
                transform.AnimScale(1, .2f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(HidePath)
                    .Run();

            })
            .SetEase(Ease.InBack)
            .SetDelay(delay)
            .Run();
    }

    private void HidePath()
    {
        transform.AnimScale(.8f, .2f)
            .OnComplete(() =>
            {
                meshRenderer.sharedMaterial = Const.Materials.slotsMaterials[(int)slotRole];
                transform.AnimScale(1f, .2f)
                    .SetEase(Ease.OutBack)
                    .Run();
            })
            .SetEase(Ease.InBack)
            .Run();
    }

    public void OnInputDown()
    {
        transform.AnimScale(.9f, .25f)
            .Run();
    }

    public void OnInputUp(bool isStillSelected)
    {
        transform.AnimScale(1f, .25f)
            .Run();
    }
}