using System;
using UnityEngine;

public class InputMgr : MonoBehaviourSingleton<InputMgr>
{
    public static bool InputDown { get; private set; } = false;
    public static bool InputUp { get; private set; } = false;
    public BoardSlot TouchedSlot { get; private set; }
    public float TouchLength { get; private set; }


    public Vector3 GetInputWorldPosition
        => Physics.Raycast(GetCameraRay(), out var hit, 100) ? hit.point : Vector3.zero;

    public BoardSlot GetNowHoveredSlot
        => Physics.Raycast(GetCameraRay(), out var hit, 100) ? hit.collider.GetComponent<BoardSlot>() : null;

    public static Vector2 InputPosition => new(Input.mousePosition.x, Input.mousePosition.y);
    public static Action OnTouchDown = null;
    public static Action OnTouchUp = null;
    public static Action<float> OnLongPressDone = null;
    // action that retuns how long button was hold 
    
    
    private bool _downInvoked, _upInvoked;
    private void Update()
    {
        InputDown = Input.GetMouseButtonDown(0);
        InputUp = Input.GetMouseButtonUp(0);

        if (InputDown)
        {
            OnTouchDown?.Invoke();
            _downInvoked = true;
            _count = true;
        }
        else if (_downInvoked)
            _downInvoked = false;

        if (InputUp)
        {
            OnTouchUp?.Invoke();
            _upInvoked = true;
        }
        else if (_upInvoked)
            _upInvoked = false;

        CountTouchDuration();
        CheckTouchedSlot();
    }

    private bool _count;
    private void CountTouchDuration()
    {
        if (!_count)
            return;
        
        if (InputUp && _count)
        {
            OnLongPressDone?.Invoke(TouchLength);
            TouchLength = 0f;
            _count = false;
            return;
        }

        TouchLength += Time.deltaTime;
    }

    private BoardSlot _lastTouched;
    private void CheckTouchedSlot()
    {
        if (!InputDown && !InputUp)
            return;

        if (!Physics.Raycast(GetCameraRay(), out var hit, 100))
            return;
        
        _lastTouched = TouchedSlot;
        TouchedSlot = GetNowHoveredSlot;
        
        if (TouchedSlot != null && InputDown)
        {
            TouchedSlot.OnInputDown();
        }

        if (!InputUp) 
            return;
        
        _lastTouched.OnInputUp(TouchedSlot == _lastTouched);
            
        _lastTouched = null;
        TouchedSlot = null;
    }
    

    private Ray GetCameraRay()
    {
        return GMgr.Instance.mainCamera.ScreenPointToRay(InputPosition);
    }
}