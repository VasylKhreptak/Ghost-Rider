using UnityEngine;

public class CameraFieldOfViewSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;

    #region MnooBehaviour

    private void OnValidate()
    {
        _camera ??= GetComponent<Camera>();
    }

    #endregion

    public void SetFieldOfView(float value)
    {
        _camera.fieldOfView = value;
    }
}
