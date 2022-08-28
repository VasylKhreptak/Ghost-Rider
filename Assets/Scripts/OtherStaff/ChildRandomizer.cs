using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ChildRandomizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private MonoEvent _monoEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform = GetComponent<Transform>();
        _monoEvent = GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += Randomize;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= Randomize;
    }

    #endregion

    private void Randomize()
    {
        int childCount = _transform.childCount;

        int childIndexToEnable = Random.Range(0, childCount);

        for (int i = 0; i < childCount; i++)
        {
            if (i == childIndexToEnable)
            {
                _transform.GetChild(i).gameObject.SetActive(true);
                
                continue;
            }

            _transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
