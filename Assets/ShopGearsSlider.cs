public class ShopGearsSlider : CarSpecificationSlider
{
    public override void UpdateValue()
    {
        _smoothSlider.value = _mainCarSpawner.CurrentCar.CarSpecification.Gears;
    }
}
