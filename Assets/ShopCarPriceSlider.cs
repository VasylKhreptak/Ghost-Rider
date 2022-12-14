using UnityEngine;

public class ShopCarPriceSlider : CarSpecificationSlider
{
    public override void UpdateValue()
    {
        _smoothSlider.value = _mainCarSpawner.CurrentCar.CarSpecification.Price;
    }
}
