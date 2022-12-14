public class ShopBreakTorqueSlider : CarSpecificationSlider
{
    public override void UpdateValue()
    {
        _smoothSlider.value = _mainCarSpawner.CurrentCar.CarSpecification.BreakTorque;
    }
}
