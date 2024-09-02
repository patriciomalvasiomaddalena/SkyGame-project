using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
   [SerializeField] private float _Maxhealth;
   [SerializeField] private float _Health;
   [SerializeField] private bool IsAlive = true;

    public delegate void OnDeath();
    public event OnDeath OnDeathEvent;

    public delegate void Revival();
    public event OnDeath OnReviveEvent;

    private void Start()
    {
        _Health = _Maxhealth;
    }

    public void SetHealth(float health)
    {
        _Maxhealth = health;
        _Health= health;
    }

    public float TakeDmg(float damage)
    {
        if(damage < 0)
        {
            return 0;
        }
        _Health -= damage;

        if(_Health <= 0 && IsAlive)
        {
            IsAlive = false;
            Death();
            return 0;
        }
        return _Health;
    }

    private void Death()
    {
        OnDeathEvent?.Invoke();

        enabled = false;
    }

    public void Revive()
    {
        IsAlive = true;
        OnReviveEvent?.Invoke();
    }

}
