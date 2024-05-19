using UnityEngine.UI;

public class GameScreen : MenuItem
{
    public Button CheckPath;
    
    // here need to be control if there is start and finish point, 
    // if there isn't then dont allow to start finding path 
    
    public override void Initialize()
    {
        CheckPath.onClick.AddListener(Mgr.Instance.RunPathFinding);
    }
}