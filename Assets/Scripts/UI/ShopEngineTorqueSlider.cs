public class ShopEngineTorqueSlider : CarSpecificationSlider
{
    public override void UpdateValue()
    {
        _smoothSlider.value = _mainCarSpawner.CurrentCar.CarSpecification.EngineTorque;
    }
}
