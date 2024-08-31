using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Fidgety.CustomAnimation;

public class GameScreen : MenuItem
{
    [SerializeField] private Button run;
    [SerializeField] private Button reset;
    [SerializeField] private Button controls;
    [SerializeField] private Transform errorNotClear;
    [SerializeField] private float animTime = .5f;
    
    private bool _working;
    
    public override void Initialize()
    {
        run.onClick.AddListener(Mgr.Instance.RunPathFinding);
        reset.onClick.AddListener(Mgr.Instance.ResetDemo);
        controls.onClick.AddListener(() => UIMgr.Instance.SelectScreen(NowScreen.Controls));
    }

    public void ShowPathNotClear()
    {
        if (_working)
            return;

        _working = true;

        var start = errorNotClear.position;
        var target = start - new Vector3(0, 50, 0);
        
        errorNotClear.AnimMove(target, animTime * .5f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                errorNotClear.AnimMove(start, animTime * .5f)
                    .SetDelay(animTime)
                    .SetEase(Ease.InCubic)
                    .OnComplete(() => _working = false)
                    .Run();
            })
            .Run();
    }
}