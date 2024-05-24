using System;
using System.Collections;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] float _Health, _MaxHealth, _speed, _WindowDamage;
    [SerializeField] bool _CanDamage;

    private void Awake()
    {
        _CanDamage = false;
        _Health = _MaxHealth;
    }
    private void Update()
    {
        if(_Health > 0)
        {
            _Health = _Health - (1*Time.deltaTime);
            Fired();

            _WindowDamage = _WindowDamage - (1 * Time.deltaTime);
            if (_WindowDamage <= 0)
            {
                _CanDamage = true;
            }
        }
        else
        {
            TestBulletFactory.Instance.ReturnObjectToPool(this);
            TurnOff(this);
        }
    }
    public void ExtraDir(float dir)
    {
       if(dir != 0) 
       {
            this.transform.rotation *= Quaternion.Euler(0, 0, dir);
       }
    }

    public void Fired()
    {
        this.transform.position += _speed * Time.deltaTime * transform.up;
    }
    private void ResetVal()
    {
        _Health = _MaxHealth;
        _CanDamage = false;
        _WindowDamage = 0.2f;
    }

    public static void TurnOn(TestBullet b)
    {
        b.ResetVal();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(TestBullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.CompareTag("ShipPart") && _CanDamage == true && Other.gameObject.activeSelf == true)
        {
            HealthComponent _EnemyHealth = Other.GetComponent<HealthComponent>();
            if (_EnemyHealth != null)
            {
                float damagedealt = _EnemyHealth.TakeDmg(_Health);
                _Health -= damagedealt;
                print("dealtdamage: " + damagedealt);
            }
        }
    }
}