using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetAnchorPreset(this RectTransform rectTransform, AnchorPreset preset)
    {
        rectTransform.anchorMin = preset.min;
        rectTransform.anchorMax = preset.max;
        rectTransform.anchoredPosition = preset.anchoredPosition;
    }
}
