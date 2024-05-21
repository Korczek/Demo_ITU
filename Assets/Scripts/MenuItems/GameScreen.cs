using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MenuItem
{
    [SerializeField] private Button run;
    [SerializeField] private Button reset;
    
    // here need to be control if there is start and finish point, 
    // if there isn't then dont allow to start finding path 
    
    public override void Initialize()
    {
        run.onClick.AddListener(Mgr.Instance.RunPathFinding);
        reset.onClick.AddListener(Mgr.Instance.ResetDemo);
    }
}