using UnityEngine;
using Fidgety.CustomAnimation;

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
        if (GetComponent<AnimController>())
        {
            
        }
    }

    public void Initialize(float delay, SlotRole role)
    {
        slotRole = role;
        meshRenderer.sharedMaterial = Const.Materials.slotsMaterials[(int)role];
        transform.localScale = Vector3.zero;
        transform.AnimScale(1, .25f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .Run();
        
        if (role == SlotRole.Start)
            Mgr.Instance.SetStartPoint(this);
        
        if (role == SlotRole.Finish)
            Mgr.Instance.SetFinishPoint(this);
    }

    public void Despawn(float delay)
    {
        transform.AnimScale(0, .25f)
            .SetEase(Ease.InBack)
            .SetDelay(delay)
            .OnComplete(() => SpawnMgr.Instance.Despawn(this))
            .Run();
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