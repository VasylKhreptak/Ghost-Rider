using UnityEngine;

public class AnimatorPauseStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    private float _previousStateSpeed;

    private float _previousSpeed;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    #endregion

    public void SetPauseState(bool enabled)
    {
        if (enabled)
        {
            _animator.speed = _previousSpeed == 0 ? 1 : _previousSpeed;
        }
        else
        {
            _previousSpeed = _animator.speed;
            _animator.speed = 0;
        }
    }
}
