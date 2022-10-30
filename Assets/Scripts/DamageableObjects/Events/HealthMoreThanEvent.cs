public class HealthMoreThanEvent : HealthValueEvent
{
	protected override bool HasAppropriateHealth(float health)
	{
		return health > _targetHealth;
	}
}
