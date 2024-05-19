using System;
using UnityEngine;

public class InputMgr : MonoBehaviourSingleton<InputMgr>
{
    public static bool InputDown { get; private set; } = false;
    public static bool InputUp { get; private set; } = false;
    public BoardSlot TouchedSlot { get; private set; }
    public bool InputLocked { get; set; } = true;

    public static Action OnTouchDown = null;
    public static Action OnTouchUp = null;
    

    public Vector3 GetInputWorldPosition
        => Physics.Raycast(GetCameraRay(), out var hit, 100) ? hit.point : Vector3.zero;

    public BoardSlot NowHoveredSlot
        => !Physics.Raycast(GetCameraRay(), out var hit, 100) ? null : hit.collider.GetComponent<BoardSlot>();

    public static Vector2 InputPosition => new(Input.mousePosition.x, Input.mousePosition.y);
    // action that retuns how long button was hold 
    
    
    private void Update()
    {
        if (InputLocked)
            return;
        
        InputDown = Input.GetMouseButtonDown(0);
        InputUp = Input.GetMouseButtonUp(0);

        if (InputDown)
        {
            OnTouchDown?.Invoke();
        }

        if (InputUp)
        {
            OnTouchUp?.Invoke();
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