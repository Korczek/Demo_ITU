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
    {
        // im getting only those that i'll be using for this demo 
        switch (ease)
        {
            // cubic
            case Ease.InCubic: return Easing.InCubic(t);
            case Ease.OutCubic: return Easing.OutCubic(t);
            case Ease.InOutCubic: return Easing.InOutCubic(t);
            
            // back
            case Ease.InBack: return Easing.InBack(t);
            case Ease.OutBack: return Easing.OutBack(t);
            case Ease.InOutBack: return Easing.InOutBack(t);
            
            // i don't think that i need more ... 
            
            default: return Easing.Linear(t);
        }
    }
}