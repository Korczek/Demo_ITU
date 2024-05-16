using System;
using UnityEngine;

public class GMgr : MonoBehaviourSingleton<GMgr>
{
    public override void Awake()
    {
        base.Awake();
        
        BoardGenerator.Instance.GenerateGrid();
    }
    
}
