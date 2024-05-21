using System;
using UnityEngine;

public class InputMgr : MonoBehaviourSingleton<InputMgr>
{
    public static bool InputDown { get; private set; } = false;
    public static bool InputUp { get; private set; } = false;
    public BoardSlot TouchedSlot { get; private set; }
    public bool InputLocked { get; set; } = true;

    public static Action OnLeftButtonDown = null;
    public static Action OnLeftButtonUp = null;

    public static Action OnRightButtonDown = null;
    public static Action OnRightButtonUp = null;

    public static Action OnScrollWheelUp = null;
    public static Action OnScrollWheelDown = null;

    public static Vector2 InputPosition => new(Input.mousePosition.x, Input.mousePosition.y);
    
    public Vector3 GetInputWorldPosition
        => Physics.Raycast(GetCameraRay(), out var hit, 100) ? hit.point : Vector3.zero;

    public BoardSlot NowHoveredSlot
        => !Physics.Raycast(GetCameraRay(), out var hit, 100) ? null : hit.collider.GetComponent<BoardSlot>();

    
    private void Update()
    {
        if (InputLocked || UIMgr.nowSelectedScreen == NowScreen.Controls)
            return;
        
        InputDown = Input.GetMouseButtonDown(0);
        InputUp = Input.GetMouseButtonUp(0);

        if (Input.GetMouseButtonDown(1))
            OnRightButtonDown?.Invoke();
        
        if (Input.GetMouseButtonUp(1))
            OnRightButtonUp?.Invoke();

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            OnScrollWheelUp?.Invoke();

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            OnScrollWheelDown?.Invoke();

        if (InputDown)
        {
            OnLeftButtonDown?.Invoke();
        }

        if (InputUp)
        {
            OnLeftButtonUp?.Invoke();
        }

        CheckTouchedSlot();
    }


    private BoardSlot _lastTouched;
    private void CheckTouchedSlot()
    {
        if (!InputDown && !InputUp)
            return;

        if (!Physics.Raycast(GetCameraRay(), out var hit, 100))
            return;
        
        var nowSlot = hit.collider.GetComponent<BoardSlot>();

        if (nowSlot == null)
        {
            ClearTouch();
            return;
        }
        
        _lastTouched = TouchedSlot;
        TouchedSlot = nowSlot;
        
        if (InputDown)
        {
            TouchedSlot.OnInputDown();
        }

        if (!InputUp) 
            return;
        
        ClearTouch();
    }

    private void ClearTouch()
    {
        if (_lastTouched != null)
            _lastTouched.OnInputUp(false);

        if (TouchedSlot != null)
            TouchedSlot.OnInputUp(false);
        
        _lastTouched = null;
        TouchedSlot = null;
    }

    private static Ray GetCameraRay()
    {
        return Mgr.Instance.mainCamera.ScreenPointToRay(InputPosition);
    }
}