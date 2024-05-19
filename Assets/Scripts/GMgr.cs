using System;
using UnityEngine;

public class GMgr : MonoBehaviourSingleton<GMgr>
{
    private BoardSlot _start, _finish;
    private void Start()
    {
        Board.Instance.GenerateGrid();
    }
    
    // make ui element that will popup when user holds mouse clicked on element for some time
    // and dissapears when player remove click selected object 
    // if coursor will be above one of the objects it will be chosen else, nothing happens 

    public void SetStartPoint(BoardSlot start)
    {
        _start = start;
    }

    public void SetFinishPoint(BoardSlot finish)
    {
        _finish = finish;
    }

    public void RunPathFinding()
    {
        if (_start == null || _finish == null)
        {
            Debug.Log("There is no start, finish or both");
            return;
        }

        var path = PathFinder.Instance.FindPath(_start, _finish);

        foreach (var p in path)
        {
            Debug.Log(p.gridPos);
        }
    }
}
