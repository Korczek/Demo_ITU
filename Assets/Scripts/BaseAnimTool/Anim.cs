using System;
using UnityEngine;
using System.Collections;

public enum RunOption
{
    Move,
    MoveLocal,
    Rotate,
    Scale
}

public class Anim 
{
    private readonly RunOption _runOption;
    private readonly Transform _tr;
    private readonly float _inTime;
    private readonly Vector3 _targetV3;
    
    private AnimController _controller;
    private Coroutine _workingCr;
    private Action _onStart = null;
    private Action _onComplete = null;
    private float _delay = 0f;



    #region Conditions

    public bool IsThisSameRunOption(RunOption runOption)
        => IsItMoveType(_runOption) && IsItMoveType(runOption) || _runOption == runOption;

    #endregion
    
    
    #region Run, Stop & constructors

    public Anim(RunOption runOption, Transform transform, Vector3 targetV3, float animTime, AnimController ctrl)
    {
        _tr = transform;
        _runOption = runOption;
        _targetV3 = targetV3;
        _inTime = animTime;
        _controller = ctrl;
    }


    public Anim Run()
    {
        _workingCr = _controller.StartCoroutine(Runner());
        return this;
    }

    public void Stop(bool complete)
    {
        _controller.StopCoroutine(_workingCr);
        _controller.RemoveAnim(this);

        if (!complete)
            return;
        
        switch (_runOption)
        {
            case RunOption.Move:
                _tr.position = _targetV3;
                break;
            case RunOption.MoveLocal:
                _tr.localPosition = _targetV3;
                break;
            case RunOption.Rotate:
                _tr.eulerAngles = _targetV3;
                break;
            case RunOption.Scale:
                _tr.localScale = _targetV3;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region Runner

    private IEnumerator GetTargetedMover()
        => _runOption switch
        {
            RunOption.Move => MoveCr(),
            RunOption.MoveLocal => MoveLocalCR(),
            RunOption.Rotate => RotateCR(),
            RunOption.Scale => ScaleCr(),
            _ => MoveCr()
        };
    
    private IEnumerator Runner()
    {
        _onStart?.Invoke();

        if (_delay > 0) yield return new WaitForSeconds(_delay);

        yield return _controller.StartCoroutine(GetTargetedMover());
        
        yield return null;
        
        _onComplete?.Invoke();

        Stop(true);
    }

    #endregion
    
    // Modifiers 
    
    #region OnStart

    public Anim OnStart(Action onStart)
    {
        _onStart = onStart;
        return this;
    }

    #endregion

    #region OnComplete

    public Anim OnComplete(Action onComplete)
    {
        _onComplete = onComplete;
        return this;
    }

    #endregion

    #region SetDelay

    public Anim SetDelay(float delay)
    {
        _delay = delay;
        return this;
    }

    #endregion

    #region SetEase

    private Ease _ease = Ease.Linear;
    public Anim SetEase(Ease ease)
    {
        _ease = ease;
        return this;
    }

    #endregion
    
    // Animations 
    
    #region Move

    private IEnumerator MoveCr()
    {
        var startV3 = _tr.position;
        var tPass = 0f;
        while (tPass <= _inTime)
        {
            var e = EasingMgr.GetEase(_ease, tPass / _inTime);
            _tr.position = Vector3.LerpUnclamped(startV3, _targetV3, e);
            tPass += Time.deltaTime;
            yield return null;
        }
    }
    
    #endregion

    #region MoveLocal

    private IEnumerator MoveLocalCR()
    {
        var startV3 = _tr.localPosition;
        var tPass = 0f;
        while (tPass <= _inTime)
        {
            var e = EasingMgr.GetEase(_ease, tPass / _inTime);
            _tr.localPosition = Vector3.LerpUnclamped(startV3, _targetV3, e);
            tPass += Time.deltaTime;
            yield return null;
        }
    }

    #endregion

    #region Rotate

    private IEnumerator RotateCR()
    {
        var startEuler = _tr.eulerAngles;
        var tPass = 0f;
        while (tPass <= _inTime)
        {
            var e = EasingMgr.GetEase(_ease, tPass / _inTime);
            _tr.eulerAngles = Vector3.LerpUnclamped(startEuler, _targetV3, e);
            tPass += Time.deltaTime;
            yield return null;
        }
    }

    #endregion

    #region Scale

    private IEnumerator ScaleCr()
    {
        var startScale = _tr.localScale;
        var tPass = 0f;
        while (tPass <= _inTime)
        {
            var e = EasingMgr.GetEase(_ease, tPass / _inTime);
            _tr.localScale = Vector3.LerpUnclamped(startScale, _targetV3, e);
            tPass += Time.deltaTime;
            yield return null;
        }
    }

    #endregion


    #region Statics

    private static bool IsItMoveType(RunOption runOption)
        => runOption is RunOption.Move or RunOption.MoveLocal;

    #endregion
}