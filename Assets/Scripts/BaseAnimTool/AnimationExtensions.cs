using UnityEngine;

public static class AnimationExtensions 
{
    public static Anim AnimMove(this Transform transform, Vector3 targetPosition, float animTime)
        => GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Move, transform, targetPosition, animTime);

    public static Anim AnimMoveLocal(this Transform transform, Vector3 targetPosition, float animTime)
    => GetOrCreateAnimController(transform.gameObject)
        .StartAnimation(RunOption.MoveLocal, transform, targetPosition, animTime);

    public static Anim AnimRotate(this Transform transform, Vector3 targetEulerAngles, float animTime)
        => GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Rotate, transform, targetEulerAngles, animTime);
    
    public static Anim AnimScale(this Transform transform, float scale, float animTime)
        => GetOrCreateAnimController(transform.gameObject)
            .StartAnimation(RunOption.Scale, transform, scale, animTime);

    public static void AnimBreak(this Transform tr, bool setOnTarget = false)
        => GetOrCreateAnimController(tr.gameObject)
            .StopAnimation(setOnTarget);
    
    private static AnimController GetOrCreateAnimController(GameObject go)
    {
        var mgr = go.GetComponent<AnimController>();
        if (mgr == null) mgr = go.AddComponent<AnimController>();
        return mgr;
    }
}