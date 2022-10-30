public class HealthLessThanEvent : HealthValueEvent
{
    protected override bool HasAppropriateHealth(float health)
    {
        return health < _targetHealth;
    }
}
