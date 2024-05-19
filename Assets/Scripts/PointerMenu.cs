using System;
using UnityEngine;
using UnityEngine.UI;

public class PointerMenu : MonoBehaviourSingleton<PointerMenu>
{
    [Space(10)] 
    
    [Header("Pointer Elements:")]
    public Transform pointerTransform;
    public Image pointerFillImage;
    public float pointerLoadTime = 1f;
    
    public bool PointerActive { get; private set; } = false;

    private bool _displayMenu;
    private BoardSlot _touchStartSlot;
    
    private void Start()
    {
        InputMgr.OnTouchDown += ShowPointer;
        InputMgr.OnLongPressDone += OnLongPressDone;
        
        pointerTransform.localScale = Vector3.zero;
    }
    
    private void Update()
    {
        if (!PointerActive)
            return;
        
        UpdatePointer(InputMgr.Instance.TouchLength / pointerLoadTime);
    }


    private void OnLongPressDone(float pressLength)
    {
        Debug.Log("hide pointer");
        _displayMenu = pressLength >= pointerLoadTime && _touchStartSlot == InputMgr.Instance.GetNowHoveredSlot;
        HidePointer();
    }
    
    private void ShowPointer()
    {
        Debug.Log("show pointer ");
        _touchStartSlot = InputMgr.Instance.GetNowHoveredSlot;
        
        pointerFillImage.fillAmount = 0;
        pointerTransform.position = InputMgr.Instance.GetInputWorldPosition;
        pointerTransform.localScale = Vector3.zero;

        PointerActive = true;
        
        pointerTransform.AnimScale(1, .25f)
            .SetEase(Ease.OutBack)
            .Run();
    }

    public void HidePointer()
    {
        if (_displayMenu)
        {
            // show menu 
            Debug.Log("menu should be displayed ");
        }
        
        
        PointerActive = false;
        pointerTransform.AnimScale(0, .25f)
            .SetEase(Ease.InBack)
            .Run();
    }

    private void UpdatePointer(float fillPct)
    {
        pointerFillImage.fillAmount = fillPct;
    }
}