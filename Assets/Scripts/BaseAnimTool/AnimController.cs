using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimController : MonoBehaviour 
{
    private readonly List<Anim> _allAnims = new();

    public Anim StartAnimation(RunOption ro, Transform tr, Vector3 targetV3, float animTime)
    {
        StopThisSame(ro);
        
        var anim = new Anim(ro, tr, targetV3, animTime, this);
        _allAnims.Add(anim);
        return anim;
    }

    public Anim StartAnimation(RunOption ro, Transform tr, float scale, float animTime)
        => StartAnimation(ro, tr, Vector3.one * scale, animTime);

    public Anim StartWait(RunOption ro, float animTime, Action toRunOnFinish)
    {
        StopThisSame(ro);
        var anim = new Anim(ro, toRunOnFinish, animTime, this);
        _allAnims.Add(anim);
        return anim;
    }

    private void StopThisSame(RunOption ro)
    {
        for (var i = 0; i < _allAnims.Count; i++)
            if (_allAnims[i].IsThisSameRunOption(ro))
                _allAnims[i].Stop(false);
    }

    private void StopAll(bool finish)
    {
        for (var i = 0; i < _allAnims.Count; i++)
            _allAnims[i].Stop(finish);
    }

    public void RemoveAnim(Anim anim) => _allAnims.Remove(anim);

    public void StopAnimation(bool finishMovement) => StopAll(finishMovement);
}