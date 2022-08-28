public static class ColorExtensions
{
    public static UnityEngine.Color WithAlpha(this UnityEngine.Color color, float alphaValue)
    {
        return new UnityEngine.Color(color.r, color.g, color.b, alphaValue);
    }
}