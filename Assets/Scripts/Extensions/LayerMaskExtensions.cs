using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool ContainsLayer(this LayerMask layerMask, int layerID)
    {
        return (layerMask.value & (1 << layerID)) > 0;
    }
}
