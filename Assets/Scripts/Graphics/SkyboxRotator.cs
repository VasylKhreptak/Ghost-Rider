using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private float _rotateSpeed;

    private Material _skybox;

    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    #region MonoBehaviour

    private void Awake()
    {
        _skybox = RenderSettings.skybox;
    }

    private void Update()
    {
        float _currentRotation = _skybox.GetFloat(Rotation);
        _skybox.SetFloat(Rotation, _currentRotation + _rotateSpeed * Time.deltaTime);
    }

    #endregion
}
