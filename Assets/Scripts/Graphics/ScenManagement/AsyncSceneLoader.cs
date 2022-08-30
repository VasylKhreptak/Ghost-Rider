using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _event;

    [Header("Preferences")]
    [SerializeField] private int _sceneID;

    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _event.onMonoCall += LoadScene;
    }

    private void OnDestroy()
    {
        _event.onMonoCall -= LoadScene;
    }

    #endregion

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(_sceneID);
    }
}
