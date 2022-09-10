using UnityEngine;

public class NoiseRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")]
    [SerializeField] private Vector3 _amplitude;
    [SerializeField] private Vector3 _frequency;

    private Quaternion _startRotation;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Awake()
    {
        _startRotation = _transform.rotation;
    }

    private void Update()
    {
        if (_amplitude == Vector3.zero) return;
        
        Rotate();
    }

    #endregion

    private void Rotate()
    {
        float dx = (float)NoiseS3D.Noise(Time.time * _frequency.x, 0f, 0f) * _amplitude.x;
        float dy = (float)NoiseS3D.Noise(0f, Time.time * _frequency.y, 0f) * _amplitude.y;
        float dz = (float)NoiseS3D.Noise(0f, 0f, Time.time * _frequency.z) * _amplitude.z;

        Quaternion rotation = _startRotation;

        rotation = Quaternion.AngleAxis(dx, _transform.right) * rotation;
        rotation = Quaternion.AngleAxis(dy, _transform.up) * rotation;
        rotation = Quaternion.AngleAxis(dz, _transform.forward) * rotation;
        
        _transform.rotation = rotation;
    }
}
