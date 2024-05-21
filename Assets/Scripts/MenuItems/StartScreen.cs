using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MenuItem
{
    // important elements 
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button runButton;
    [SerializeField] private Transform errorMessage;

    [Space(20)] [Header("Input texts:")] 
    [SerializeField] private TMP_InputField _width;
    [SerializeField] private TMP_InputField _length;
    
    public override void Initialize()
    {
        InitializeButtons();
        errorMessage.localScale = Vector3.zero;
    }

    
    private void InitializeButtons()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var option = i;
            buttons[i].onClick
                .AddListener(() => Mgr.Instance.StartDemo(Const.GetPreparedLevel(option)));
        }

        runButton.onClick.AddListener(TryToRun);
    }


    private void TryToRun()
    {
        int.TryParse(_width.text, out int w);
        int.TryParse(_length.text, out int l);

        Debug.Log($"width: {w}, wText: {_width.text}, length: {l}, lText: {_length.text}");
        
        if (w < 1 || l < 1)
        {
            SetErrorVisible(true);
            return;
        }

        var length = w * l;
        var map = new int[length];
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
                map[i] = 0;
            else if (i == length - 1)
                map[i] = 1;
            else
                map[i] = 2;
        }

        Mgr.Instance.StartDemo(new MapInitData
        {
            Width = w,
            Length = l,
            StartInstruction = map
        });
    }

    private bool _errorStatus;
    private void SetErrorVisible(bool condition)
    {
        if (_errorStatus == condition)
            return;
        
        errorMessage.AnimScale(condition ? 1 : 0, .3f)
            .SetEase(condition ? Ease.OutBack : Ease.InBack)
            .Run();
    }
    
}