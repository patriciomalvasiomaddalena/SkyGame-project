using System;
using System.Collections;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] float _Health, _MaxHealth,_speed;
    [SerializeField] LayerMask _HitMask;
    [SerializeField] String _SpawnerName;
 
    private void Start()
    {
        _Health = 0;

        _SpawnerName = GetComponentInParent<Transform>().name;
    }
    private void Update()
    {
        if(_Health > 0)
        {
            _Health = _Health - (1*Time.deltaTime);
            Fired();
        }
        else
        {
            TestBulletFactory.Instance.ReturnObjectToPool(this);
            TurnOff(this);
        }
    }

    public void Fired()
    {
        this.transform.position += _speed * Time.deltaTime * transform.up;
    }

    private void ResetVal()
    {
        _Health = _MaxHealth;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ShipPart"))
        {
           HealthComponent _EnemyHealth = collision.GetComponent<HealthComponent>();
            if(_EnemyHealth != null)
            {

                float  damagedealt= _EnemyHealth.TakeDmg(_Health);
                _Health -= damagedealt;
                print("dealtdamage: " + damagedealt);
            }
        }
    }
}