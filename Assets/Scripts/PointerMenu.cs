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

    [Space(10)] 
    [Header("Menu Elements:")]
    public Transform menuTransform;

    private bool _pointerActive;
    private bool _displayMenu;
    private BoardSlot _touchStartSlot;
    private BoardSlot _selectedSlot;
    
    private bool _menuActive;
    
    private void Start()
    {
        InputMgr.OnLeftButtonDown += ShowPointer;
        InputMgr.OnLeftButtonUp += HidePointer;
        
        PreparePointer();
        PrepareMenu();
    }

    private float _pointerTime, _nowTick;
    private readonly float _slotCheckTick = .1f; // optimisation only
    private void Update()
    {
        if (!_pointerActive)
            return;

        UpdatePointer(_pointerTime / pointerLoadTime);
        _displayMenu = _pointerTime >= pointerLoadTime;
        _pointerTime += Time.deltaTime;

        if (_nowTick > Time.time)
            return;

        _nowTick = Time.time + _slotCheckTick;
        
        if (_touchStartSlot == InputMgr.Instance.NowHoveredSlot)
            return;
        
        _displayMenu = false;
        HidePointer();
    }



    #region Pointer

    
    private void PreparePointer()
    {
        pointerTransform.localScale = Vector3.zero;
    }
    
    private void ShowPointer()
    {
        if(_menuActive)
            HideMenu();
        
        _touchStartSlot = InputMgr.Instance.NowHoveredSlot;
        
        if (_touchStartSlot == null)
            return;
        
        _pointerTime = 0;
        
        pointerFillImage.fillAmount = 0;
        pointerTransform.position = InputMgr.Instance.GetInputWorldPosition;
        pointerTransform.localScale = Vector3.zero;

        
        _pointerActive = true;
        
        pointerTransform.AnimScale(1, .25f)
            .SetEase(Ease.OutBack)
            .Run();
    }

    private void HidePointer()
    {
        if (!_pointerActive) return;

        _pointerActive = false;
        pointerTransform.AnimScale(0, .25f)
            .SetEase(Ease.InBack)
            .Run();
        
        if (!_displayMenu) return;
        
        ShowMenu();
    }

    private void UpdatePointer(float fillPct)
    {
        pointerFillImage.fillAmount = fillPct;
    }
    
    #endregion


    #region Slot switch menu

    
    #region Functions for buttons

    public void OnStartPressed() => ButtonPressed(SlotRole.Start);
    public void OnFinishPressed() => ButtonPressed(SlotRole.Finish);
    public void OnPathPressed() => ButtonPressed(SlotRole.Path);
    public void OnObstaclePressed() => ButtonPressed(SlotRole.Obstacle);

    #endregion
    
    private void ButtonPressed(SlotRole onButtonPressed)
    {
        _selectedSlot.Initialize(0f, onButtonPressed);
    }
    
    
    private void PrepareMenu()
    {
        menuTransform.localScale = Vector3.zero;
    }

    private void ShowMenu()
    {
        _selectedSlot = _touchStartSlot;
        _menuActive = true;

        menuTransform.position = _touchStartSlot.transform.position;
        
        menuTransform.AnimScale(1, .25f)
            .SetEase(Ease.OutBack)
            .Run();
    }

    private void HideMenu()
    {
        _menuActive = false;
        menuTransform.AnimScale(0, .25f)
            .SetEase(Ease.InBack)
            .Run();
    }
    
    #endregion
}