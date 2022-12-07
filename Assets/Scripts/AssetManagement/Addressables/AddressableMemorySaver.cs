using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableMemorySaver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private AssetReference _assetReference;

    [Header("Preferences")]
    [SerializeField] private float _unloadDelay = 2f;

    private GameObject _child;

    private Tween _unloadWaitTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void OnEnable()
    {
        LoadAsync();
    }

    private void OnDisable()
    {
        Unload();
    }

    private void OnDestroy()
    {
        _unloadWaitTween.Kill();
    }

    #endregion

    private void LoadAsync()
    {
        _unloadWaitTween.Kill();

        if (_child == null)
        {
            _assetReference.InstantiateAsync(_transform).Completed += OnLoaded;

            void OnLoaded(AsyncOperationHandle<GameObject> asyncOperation)
            {
                _child = asyncOperation.Result;

                if (gameObject.activeSelf == false)
                {
                    Unload();
                }
            }
        }
        else
        {
            _child.SetActive(true);
        }
    }

    private void Unload()
    {
        _unloadWaitTween = this.DOWait(_unloadDelay).OnComplete(() =>
        {
            Addressables.Release(_child);

            _child = null;
        });
    }
}
