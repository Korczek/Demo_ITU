using System;
using UnityEngine;

public class InputMgr : MonoBehaviourSingleton<InputMgr>
{
    public static bool InputDown { get; private set; } = false;
    public static bool InputUp { get; private set; } = false;

    public static Action OnTouchDown = null;
    public static Action OnTouchUp = null;
    
    
    private bool _downInvoked, _upInvoked;
    private void Update()
    {
        InputDown = Input.GetMouseButtonDown(0);
        InputUp = Input.GetMouseButtonUp(0);

        if (InputDown)
        {
            OnTouchDown?.Invoke();
            _downInvoked = true;
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
    }
}