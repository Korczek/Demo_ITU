using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fidgety.CustomAnimation
{
    public class AnimController : MonoBehaviour 
    {
        private readonly List<FidgetyAnim> _allAnims = new();

        public FidgetyAnim StartAnimation(RunOption ro, Transform tr, Vector3 targetV3, float animTime)
        {
            StopThisSame(ro);
        
            var anim = new FidgetyAnim(ro, tr, targetV3, animTime, this);
            _allAnims.Add(anim);
            return anim;
        }

        public FidgetyAnim StartAnimation(RunOption ro, Transform tr, float scale, float animTime)
            => StartAnimation(ro, tr, Vector3.one * scale, animTime);

        public FidgetyAnim StartWait(RunOption ro, float animTime, Action toRunOnFinish)
        {
            StopThisSame(ro);
            var anim = new FidgetyAnim(ro, toRunOnFinish, animTime, this);
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

        public void RemoveAnim(FidgetyAnim fidgetyAnim) => _allAnims.Remove(fidgetyAnim);

        public void StopAnimation(bool finishMovement) => StopAll(finishMovement);
    }
}
