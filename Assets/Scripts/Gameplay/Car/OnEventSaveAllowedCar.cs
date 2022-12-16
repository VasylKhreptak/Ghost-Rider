public class OnEventSaveAllowedCar : OnEventSaveCar
{
    protected override void SaveCar()
    {
        if (_playerDataProvider.playerData.boughtCars.Contains(_mainCarSpawner.CurrentCar.Pool))
        {
            _playerDataProvider.playerData.lastCar = _mainCarSpawner.CurrentCar.Pool;
        }
    }
}
