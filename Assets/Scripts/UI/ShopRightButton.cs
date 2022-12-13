public class ShopRightButton : CarSelectButton
{
    protected override void SelectCar()
    {
        _mainCarSpawner.Spawn(_cars.Next(_mainCarSpawner.CurrentCar.Pool));
    }
}
