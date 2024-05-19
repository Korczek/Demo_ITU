using UnityEngine;

public enum NowScreen
{
    Start,
    InGame,
    Controls,
    Null
}

public class UIMgr : MonoBehaviourSingleton<UIMgr>
{
    public MenuItem[] screens;
    
    public void InitUi()
    {
        foreach (var s in screens)
        {
            s.gameObject.SetActive(true);
            s.Initialize();
        }

        SelectScreen(NowScreen.Start);
    }
    
    public void SelectScreen(NowScreen nScreen)
    {
        for (var i = 0; i < screens.Length; i++)
        {
            screens[i].gameObject.SetActive(i == (int)nScreen);
            // run show anim and create action to init another or something like that 
            // insteas of just set active ... or move im doing more than i need 
        }
    }
}