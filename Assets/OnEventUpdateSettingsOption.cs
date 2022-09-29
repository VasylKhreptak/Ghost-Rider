using UnityEngine;

public class OnEventUpdateSettingsOption : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIUpdatableItem _updatableItem;

    [Header("Update Event")]
    [SerializeField] private MonoEvent _event;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _updatableItem ??= GetComponent<UIUpdatableItem>();
    }

    private void OnEnable()
    {
        _event.onMonoCall += UpdateValue;
    }

    private void OnDisable()
    {
        _event.onMonoCall -= UpdateValue;
    }

    #endregion

    private void UpdateValue()
    {
        _updatableItem.UpdateValue();
    }
}
