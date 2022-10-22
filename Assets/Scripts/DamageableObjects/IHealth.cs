using System;

public interface IHealth
{
	public event Action onRestore;
	public event Action<float> onAddHealth;
	public event Action<float> onSetHealth;
	public event Action<float> onTakeDamage;
	public event Action onDeath;
	
	public void Restore();

	public bool IsAlive();

	public void AddHealth(float health);
	
	public void TakeDamage(float damage);

	public float GetHealth();

	public float GetMaxHealth();
	
	public void Kill();
}
