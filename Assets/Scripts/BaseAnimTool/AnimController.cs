using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour 
{
    private readonly List<Anim> _allAnims = new();

    public Anim StartAnimation(RunOption ro, Transform tr, Vector3 targetV3, float animTime)
    {
        foreach (var aa in _allAnims)
        {
            if (aa.IsThisSameRunOption(ro))
                aa.Stop(false);
        }
        
        // start new animation 
        var anim = new Anim(ro, tr, targetV3, animTime, this);
        _allAnims.Add(anim);

        return anim;
    }

    public void RemoveAnim(Anim anim)
    {
        _allAnims.Remove(anim);
    }
    
    public void StopAnimation(bool finishMovement)
    {
        _allAnims.ForEach(a => a.Stop(finishMovement));
    }
}