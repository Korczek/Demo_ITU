using System;
using UnityEngine;

namespace Fidgety.CustomAnimation
{
    public static class FidgetyAnimationExtensions 
    {
        public static FidgetyAnim AnimMove(this Transform transform, Vector3 targetPosition, float animTime)
            => GetOrCreateAnimController(transform.gameObject)
                .StartAnimation(RunOption.Move, transform, targetPosition, animTime);

        public static FidgetyAnim AnimMoveLocal(this Transform transform, Vector3 targetPosition, float animTime)
            => GetOrCreateAnimController(transform.gameObject)
                .StartAnimation(RunOption.MoveLocal, transform, targetPosition, animTime);

        public static FidgetyAnim AnimRotate(this Transform transform, Vector3 targetEulerAngles, float animTime)
            => GetOrCreateAnimController(transform.gameObject)
                .StartAnimation(RunOption.Rotate, transform, targetEulerAngles, animTime);
    
        public static FidgetyAnim AnimScale(this Transform transform, float scale, float animTime)
            => GetOrCreateAnimController(transform.gameObject)
                .StartAnimation(RunOption.Scale, transform, scale, animTime);

        public static void AnimBreak(this Transform tr, bool setOnTarget = false)
            => GetOrCreateAnimController(tr.gameObject)
                .StopAnimation(setOnTarget);

        public static FidgetyAnim WaitAndRun(this GameObject o, Action toRunAfterDelay, float runDelayTime)
            => GetOrCreateAnimController(o)
                .StartWait(RunOption.WaitAndRun, runDelayTime, toRunAfterDelay);
    
        private static AnimController GetOrCreateAnimController(GameObject go)
        {
            var mgr = go.GetComponent<AnimController>();
            if (mgr == null) mgr = go.AddComponent<AnimController>();
            return mgr;
        }
    }
}