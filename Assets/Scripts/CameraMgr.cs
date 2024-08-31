using System;
using System.Collections;
using UnityEngine;
using Fidgety.CustomAnimation;

public class CameraMgr : MonoBehaviourSingleton<CameraMgr>
{
    [Header("Move components:")]
    [SerializeField] private Transform movePoint;
    [SerializeField] private Transform cameraTransform;

    [Space(20)] 
    [Header("Rotation:")]
    [SerializeField] private float maxCameraDown = 20f;
    [SerializeField] private float maxCameraUp = 90f;
    [SerializeField] private float rotationSmoothTime = .01f;

    [Space(20)] 
    [Header("Movement:")]
    [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private float moveSmoothTime = .01f;

    [Space(20)] [Header("Zoom:")] 
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float zoomAnimTime = .15f;
    [SerializeField] [Range(.01f, .25f)] private float zoomIncrementation = .05f;
    
    private bool _rightButtonActive;
    private bool _leftButtonActive;
    
    private Vector2 _startRightButtonPos;
    private Vector3 _targetEulerAngles;
    private Vector3 _currentRotationVelocity;

    private Vector2 _startLeftButtonPos;
    private Vector3 _targetMovePosition;
    private Vector3 _currentMoveVelocity;

    private float _zoomPct = 1f;
    private void Start()
    {
        InputMgr.OnRightButtonDown += OnRightButtonPressed;
        InputMgr.OnRightButtonUp += OnRightButtonReleased;
        
        InputMgr.OnLeftButtonDown += OnLeftButtonPressed;
        InputMgr.OnLeftButtonUp += OnLeftButtonReleased;

        InputMgr.OnScrollWheelUp += OnScrollUp;
        InputMgr.OnScrollWheelDown += OnScrollDown;
    }

    #region Camera roatation

    private void OnRightButtonPressed()
    {
        _rightButtonActive = true;
        StartCoroutine(RotateCamera());
    }

    private void OnRightButtonReleased()
    {
        _rightButtonActive = false;
    }

    private IEnumerator RotateCamera()
    {
        _startRightButtonPos = InputMgr.InputPosition;
        
        while (_rightButtonActive)
        {
            var nPos = InputMgr.InputPosition;
            var ea = movePoint.eulerAngles;

            var moveDist = _startRightButtonPos - nPos;
            _startRightButtonPos = nPos;
            
            if (ea.x >= maxCameraUp && ea.x - moveDist.y >= maxCameraUp)
                moveDist.y = 0;
            
            if (ea.x <= maxCameraDown && ea.x - moveDist.y <= maxCameraDown)
                moveDist.y = 0;

            _targetEulerAngles = ea - new Vector3(-moveDist.y, moveDist.x, 0);
            _targetEulerAngles.x = Mathf.Clamp(_targetEulerAngles.x, maxCameraDown, maxCameraUp);

            movePoint.eulerAngles = Vector3.SmoothDamp(
                movePoint.eulerAngles,
                _targetEulerAngles,
                ref _currentRotationVelocity,
                rotationSmoothTime
            );

            yield return null;
        }
    }

    #endregion

    #region Camera movement

    private void OnLeftButtonPressed()
    {
        _leftButtonActive = true;
        StartCoroutine(MoveCamera());
    }

    private void OnLeftButtonReleased()
    {
        _leftButtonActive = false;
    }

    private IEnumerator MoveCamera()
    {
        var aspectRatio = (float)Screen.width / Screen.height;
        _startLeftButtonPos = InputMgr.InputPosition;
        while (_leftButtonActive)
        {
            var nPos = InputMgr.InputPosition;
            var moveDist = (_startLeftButtonPos - nPos) * (moveSpeed * Time.deltaTime);
            _startLeftButtonPos = nPos;

            moveDist.y *= aspectRatio;
            
            var move = movePoint.right * moveDist.x + movePoint.forward * moveDist.y;
            move.y = 0;

            var mpPosition = movePoint.position;
            
            _targetMovePosition = mpPosition + move;

            movePoint.position = Vector3.SmoothDamp(
                mpPosition,
                _targetMovePosition,
                ref _currentMoveVelocity,
                moveSmoothTime
            );

            yield return null;
        }
    }

    #endregion

    #region Camera zoom
    
    private void OnScrollUp()
    {
        ZoomInOut(true);
    }

    private void OnScrollDown()
    {
        ZoomInOut(false);
    }

    private void ZoomInOut(bool zoomIn)
    {
        if (_zoomPct < 1f && zoomIn)
            _zoomPct += zoomIncrementation;

        if (_zoomPct > 0f && !zoomIn)
            _zoomPct -= zoomIncrementation;
            
        cameraTransform.AnimBreak();
        
        var target = cameraTransform.localPosition;
        target.z = Mathf.Lerp(-maxZoom, -minZoom, _zoomPct);
        
        cameraTransform.AnimMoveLocal(target, zoomAnimTime)
            .SetEase(Ease.InOutCubic)
            .Run();
    }
    #endregion
}