using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class AnimationTransformExtension 
{
    private static List<AnimController> _movers = new();
    
    public static Anim AnimMove(this Transform transform, Vector3 targetPosition, float animTime)
    {
        return GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Move, transform, targetPosition, animTime);
    }

    public static Anim AnimMoveLocal(this Transform transform, Vector3 targetPosition, float animTime)
    {
        return GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Move, transform, targetPosition, animTime);
    }

    public static Anim AnimRotate(this Transform transform, Vector3 targetEulerAngles, float animTime)
    {
        return GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Move, transform, targetEulerAngles, animTime);
    }
    
    
    private static AnimController GetOrCreateAnimController(GameObject go)
    {
        var mgr = go.GetComponent<AnimController>();
        if (mgr == null) mgr = go.AddComponent<AnimController>();
        return mgr;
    }
}