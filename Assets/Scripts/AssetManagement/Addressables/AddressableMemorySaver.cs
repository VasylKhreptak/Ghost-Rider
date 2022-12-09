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
        TryLoadAsync();
    }

    private void OnDisable()
    {
        TryUnload();
    }

    private void OnDestroy()
    {
        _unloadWaitTween.Kill();
    }

    #endregion

    private void TryLoadAsync()
    {
        _unloadWaitTween.Kill();

        if (_child == null)
        {
            _assetReference.InstantiateAsync().Completed += OnCompleted;
            
            void OnCompleted(AsyncOperationHandle<GameObject> asyncOperation)
            {
                _child = asyncOperation.Result;

                if (gameObject.activeSelf == false)
                {
                    TryUnload();
                }
            }
        }
        else
        {
            _child.SetActive(true);
        }
    }

    private void TryUnload()
    {
        _unloadWaitTween = this.DOWait(_unloadDelay).OnComplete(() =>
        {
            if (_child != null)
            {
                _child.SetActive(false);
            
                Addressables.ReleaseInstance(_child);
            }

            _child = null;
        });
    }
}
