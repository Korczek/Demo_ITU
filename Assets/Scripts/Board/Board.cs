using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviourSingleton<Board>
{
    public Transform slotsParent;
    private readonly List<BoardSlot> _allSlots = new();
    
    [Space(20)] 
    
    [Header("Slots offset")] 
    public float widthOffset;
    public float lengthOffset;

    [Space(20)]
    
    [Header("Board size")]
    public int width;
    public int length;
    
    #region Grid generation & neighbor assignment

    public void GenerateGrid()
    {
        var startPos = new Vector3(widthOffset * (width - 1) * -.5f, 0, (length - 1) * lengthOffset * -.5f);
        
        for (int i = 0; i < width; i++)
        for (int j = 0; j < length; j++)
        {
            var slot = GetNewSlot(startPos + new Vector3(i * widthOffset, 0, j * lengthOffset));
            slot.gridPos = new Vector2Int(i, j);
            AddNeighbors(slot);
            _allSlots.Add(slot);
            slot.AnimSlotIn(i * .15f);
        }
    }

    private void AddNeighbors(BoardSlot slot)
    {
        var left = _allSlots.FirstOrDefault(t => t.gridPos == slot.gridPos - new Vector2Int(1, 0));
        var down = _allSlots.FirstOrDefault(t => t.gridPos == slot.gridPos - new Vector2Int(0, 1));
        
        if (left != null)
        {
            slot.neighbors[(int)NeighborDirection.Left] = left;
            left.neighbors[(int)NeighborDirection.Right] = slot;
        }

        if (down != null)
        {
            slot.neighbors[(int)NeighborDirection.Down] = down;
            down.neighbors[(int)NeighborDirection.Up] = slot;
        }
    }

    #endregion
    



    

    public BoardSlot GetSlotByGridPos(Vector2Int slotPos)
    {
        return _allSlots.FirstOrDefault(s => s.gridPos == slotPos);
    }
    
    
    private BoardSlot GetNewSlot(Vector3 position)
    {
        return SpawnMgr.Instance.Spawn("Slot", position, Vector3.zero, slotsParent)
            .GetComponent<BoardSlot>();
    }
}