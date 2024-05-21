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
    public static NowScreen nowSelectedScreen { get; private set; }
    public MenuItem[] screens;
    private bool _controlsWasDisplayed = false;
    
    
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
        if (nScreen == NowScreen.InGame && !_controlsWasDisplayed)
        {
            nScreen = NowScreen.Controls;
            _controlsWasDisplayed = true;
        }

        nowSelectedScreen = nScreen;
        
        for (var i = 0; i < screens.Length; i++)
            screens[i].gameObject.SetActive(i == (int)nScreen);
    }
}