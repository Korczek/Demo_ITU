using UnityEngine.UIElements.Experimental;

// im leaving those commented one cause maybe later i will use them xD 
public enum Ease
{
    // Null,
    Linear,
    // InSine,
    // OutSine,
    // InOutSine,
    // InQuad,
    // OutQuad,
    // InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    // InQuart,
    // OutQuart,
    // InOutQuart,
    // InQuint,
    // OutQuint,
    // InOutQuint,
    // InExpo,
    // OutExpo,
    // InOutExpo,
    // InCirc,
    // OutCirc,
    // InOutCirc,
    // InElastic,
    // OutElastic,
    // InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    // InBounce,
    // OutBounce,
    // InOutBounce,
    // Flash,
    // InFlash,
    // OutFlash,
    // InOutFlash,
}

public static class EasingMgr
{
    public static float GetEase(Ease ease, float t)
        => ease switch
        {
            // cubic
            Ease.InCubic => Easing.InCubic(t),
            Ease.OutCubic => Easing.OutCubic(t),
            Ease.InOutCubic => Easing.InOutCubic(t),
            // back
            Ease.InBack => Easing.InBack(t),
            Ease.OutBack => Easing.OutBack(t),
            Ease.InOutBack => Easing.InOutBack(t),

            _ => Easing.Linear(t)
        };
}