public class ShopSpeedSlider : CarSpecificationSlider
{
    public override void UpdateValue()
    {
        _smoothSlider.value = _mainCarSpawner.CurrentCar.CarSpecification.MaxSpeed;
    }
}
