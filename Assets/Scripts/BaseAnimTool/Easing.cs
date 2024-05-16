using UnityEngine.UIElements.Experimental;

public enum Ease
{
    Null,
    Linear,
    InSine,
    OutSine,
    InOutSine,
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce,
    Flash,
    InFlash,
    OutFlash,
    InOutFlash,
}

public static class EasingMgr
{
    public static float GetEase(Ease ease, float t)
    {
        // im getting only those i will use for this project... 
        switch (ease)
        {
            case Ease.InCubic: return Easing.InCubic(t);
            case Ease.OutCubic: return Easing.OutCubic(t);
            case Ease.InOutCubic: return Easing.InOutCubic(t);
            default: return Easing.Linear(t);
        }
    }
}