using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float _currentHealth;
    public event Action<Target> OnDestroyed;
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        if (_currentHealth <= 0)
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
           

    }
    
}
