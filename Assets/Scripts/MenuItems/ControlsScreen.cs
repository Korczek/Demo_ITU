using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlsScreen : MenuItem
{
    public Button backButton;
    public override void Initialize()
    {
        backButton.onClick.AddListener(() => UIMgr.Instance.SelectScreen(NowScreen.InGame));
    }
}