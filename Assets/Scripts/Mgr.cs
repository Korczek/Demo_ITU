using System;
using System.Linq;
using UnityEngine;

public class Mgr : MonoBehaviourSingleton<Mgr>
{
    private BoardSlot _start, _finish;
    public Camera mainCamera { get; private set; }

    
    private void Start()
    {
        mainCamera = Camera.main;
        UIMgr.Instance.InitUi();
    }

    public void StartDemo(MapInitData mapInitData)
    {
        Board.Instance.GenerateGrid(mapInitData);
        UIMgr.Instance.SelectScreen(NowScreen.InGame);
        // set camera 
    }

    public void ResetDemo()
    {
        Board.Instance.RemoveGrid();
        UIMgr.Instance.SelectScreen(NowScreen.Start);
    }
    
    // make ui element that will popup when user holds mouse clicked on element for some time
    // and dissapears when player remove click selected object 
    // if coursor will be above one of the objects it will be chosen else, nothing happens 

    public void SetStartPoint(BoardSlot start)
    {
        if (_start != null)
            _start.Initialize(0, SlotRole.Obstacle);
        
        _start = start;
    }

    public void SetFinishPoint(BoardSlot finish)
    {
        if (_finish != null)
            _finish.Initialize(0, SlotRole.Obstacle);
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

        var delay = .1f;

        if (path == null)
        {
            PathNotClear();
            return;
        }
        for (var i = 0; i < path.Count; i++)
        {
            path[i].ShowAsPath(i*delay);
        }

    }

    private void PathNotClear()
    {
        foreach (var screen in UIMgr.Instance.screens)
        {
            if (screen is not GameScreen gameScreen) 
                continue;
            
            gameScreen.ShowPathNotClear();
            return;
        }
    }
}
