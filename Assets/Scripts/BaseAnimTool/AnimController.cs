using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour 
{
    private readonly List<Anim> _allAnims = new();

    public Anim StartAnimation(RunOption ro, Transform tr, Vector3 targetV3, float animTime)
    {
        for (var i = 0; i < _allAnims.Count; i++)
            if (_allAnims[i].IsThisSameRunOption(ro))
                _allAnims[i].Stop(false);
        
        var anim = new Anim(ro, tr, targetV3, animTime, this);
        _allAnims.Add(anim);
        return anim;
    }

    public Anim StartAnimation(RunOption ro, Transform tr, float scale, float animTime)
        => StartAnimation(ro, tr, Vector3.one * scale, animTime);


    public void RemoveAnim(Anim anim) => _allAnims.Remove(anim);

    public void StopAnimation(bool finishMovement) => _allAnims.ForEach(a => a.Stop(finishMovement));
}