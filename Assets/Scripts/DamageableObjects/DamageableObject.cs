using System;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IHealth
{
    [Header("Preferences")]
    [SerializeField] private float _maxHealth;

    [Header("Preferences")]
    [SerializeField] private bool _maxHealthOnAwake = true;

    private SafeFloat _health;

    public event Action onRestore;
    public event Action<float> onAddHealth;
    public event Action<float> onSetHealth;
    public event Action<float> onTakeDamage;
    public event Action onDeath;

    #region MonoBehaviour

    private void Awake()
    {
        if (_maxHealthOnAwake)
        {
            Restore();
        }
    }

    #endregion

    public void Restore()
    {
        SetHealth(_maxHealth);

        onRestore?.Invoke();
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void AddHealth(float health)
    {
        if (health <= 0) return;

        SetHealth(_health + health);

        onAddHealth?.Invoke(health);
    }

    private void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, _maxHealth);

        Debug.Log("Health: " + (_health));
        
        onSetHealth?.Invoke(health);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0 || IsAlive() == false) return;
        
        bool wasDead = _health == 0;

        SetHealth(_health - damage);

        onTakeDamage?.Invoke(damage);
        
        if (_health == 0 && wasDead == false)
        {
            onDeath?.Invoke();
        }
    }
    public float GetHealth() => _health;
    
    public float GetMaxHealth() => _maxHealth;
    
    public void Kill()
    {
        bool wasDead = _health == 0;
        
        SetHealth(0);
        
        if (wasDead == false)
        {
            onDeath?.Invoke();
        }
    }
}
