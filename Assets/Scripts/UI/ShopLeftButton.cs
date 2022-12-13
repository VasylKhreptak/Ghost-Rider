public class ShopLeftButton : CarSelectButton
{
    protected override void SelectCar()
    {
        _mainCarSpawner.Spawn(_cars.Previous(_mainCarSpawner.CurrentCar.Pool));
    }
}
