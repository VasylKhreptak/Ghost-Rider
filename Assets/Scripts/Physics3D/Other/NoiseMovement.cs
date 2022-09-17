using UnityEngine;

public class NoiseMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _amplitude;
    [SerializeField] private Vector3 _frequency;

    private Vector3 _startPosition;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Awake()
    {
        _startPosition = _transform.position;
    }

    private void Update()
    {
        if (_amplitude == Vector3.zero) return;

        Move();
    }

    #endregion

    private void Move()
    {
        float dx = (float)NoiseS3D.Noise(Time.time * _frequency.x, 0f, 0f) * _amplitude.x;
        float dy = (float)NoiseS3D.Noise(0f, Time.time * _frequency.y, 0f) * _amplitude.y;
        float dz = (float)NoiseS3D.Noise(0f, 0f, Time.time * _frequency.z) * _amplitude.z;

        Vector3 position = _startPosition;

        position += _transform.right * dx;
        position += _transform.up * dy;
        position += _transform.forward * dz;

        _transform.position = position;
    }
}