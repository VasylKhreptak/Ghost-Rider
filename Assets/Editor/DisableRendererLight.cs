using UnityEngine;
using UnityEngine.Rendering;

public class DisableRendererLight : MonoBehaviour
{
    #region MonoBehavoiour

    private void OnValidate()
    {
        DisableLight();
    }

    #endregion

    private void DisableLight()
    {
        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.lightProbeUsage = LightProbeUsage.Off;
        }
    }
}
