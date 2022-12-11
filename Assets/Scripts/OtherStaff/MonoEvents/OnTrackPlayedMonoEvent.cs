using UnityEngine;

public class OnTrackPlayedMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private BackgroundMusicPlayer _backgroundMusicPlayer;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _backgroundMusicPlayer ??= GetComponent<BackgroundMusicPlayer>();
    }

    private void OnEnable()
    {
        _backgroundMusicPlayer.onTrackPlayed += OnPlayed;
    }

    private void OnDisable()
    {
        _backgroundMusicPlayer.onTrackPlayed -= OnPlayed;
    }

    #endregion

    private void OnPlayed(AudioClip clip) => Invoke();
}
