using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
   [SerializeField] private float _Maxhealth;
   [SerializeField] private float _Health;

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
        this.gameObject.SetActive(false);
    }

    public void Revive()
    {
        this.gameObject.SetActive(true);
        _Health = _Maxhealth;
    }

}
