using UnityEngine.UI;

public class StartScreen : MenuItem
{
    // important elements 
    public Button[] buttons;
    
    public override void Initialize()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var option = i;
            buttons[i].onClick
                .AddListener(() => Mgr.Instance.StartDemo(Const.GetPreparedLevel(option)));
        }
    }

    
}