using UnityEngine;

public class OnEventSpawnAllowedCar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    [SerializeField] private PlayerDataProvider _playerDataProvider;

    [Header("Events")]
    [SerializeField] private MonoEvent _monoEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
        _playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
    }

    private void OnEnable()
    {
        _monoEvent.onMonoCall += SpawnAllowedCar;
    }

    private void OnDisable()
    {
        _monoEvent.onMonoCall -= SpawnAllowedCar;
    }

    #endregion

    private void SpawnAllowedCar()
    {
        if (IsCarAllowed())
        {
            _playerDataProvider.playerData.lastCar = _mainCarSpawner.CurrentCar.Pool;
        }
        else
        {
            _mainCarSpawner.Spawn(_playerDataProvider.playerData.lastCar);
        }
    }

    private bool IsCarAllowed()
    {
        return _playerDataProvider.playerData.boughtCars.Contains(_mainCarSpawner.CurrentCar.Pool);
    }
}
