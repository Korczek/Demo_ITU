using UnityEngine;

public enum NowScreen
{
    Start,
    Tutorial,
    InGame,
    Null
}

public class UIMgr : MonoBehaviourSingleton<UIMgr>
{
    public MenuItem[] screens;

    public void SelectScreen(NowScreen nScreen)
    {
        for (var i = 0; i < screens.Length; i++)
        {
            screens[i].gameObject.SetActive(i == (int)nScreen);
            
            if(i == (int)nScreen)
                screens[i].Initialize();
        }
        
        
    }
}