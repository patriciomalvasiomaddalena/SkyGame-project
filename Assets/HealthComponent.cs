using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
   [SerializeField] private float _Maxhealth;
   [SerializeField] private float _Health;

    public delegate void OnDeath();
    public event OnDeath OnDeathEvent;

    public delegate void Revival();
    public event OnDeath OnReviveEvent;

    private void Start()
    {
        _Health = _Maxhealth;
    }

    public float TakeDmg(float damage)
    {
        _Health -= damage;

        if(_Health <= 0)
        {
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
        OnReviveEvent?.Invoke();
    }

}
