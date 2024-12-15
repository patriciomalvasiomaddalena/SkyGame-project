using System;
using System.Collections;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] protected float  _MaxHealth, _speed, _WindowDamage;
    [SerializeField] protected bool _CanDamage;
    public HealthComponent _HPComp;
    public TestBulletFactory _ThisFactory; 

    private void Awake()
    {
        _CanDamage = false;
        _HPComp = GetComponent<HealthComponent>();
        _HPComp.SetHealth(_MaxHealth);
    }
    private void Update()
    {
        if(_HPComp._Health > 0)
        {
            _HPComp.TakeDmg(1 * Time.deltaTime);
            Fired();

            _WindowDamage = _WindowDamage - (1 * Time.deltaTime);
            if (_WindowDamage <= 0)
            {
                _CanDamage = true;
            }
        }
        else
        {
            _ThisFactory.ReturnObjectToPool(this);
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
    protected virtual void ResetVal()
    {
        _HPComp.Revive();
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

    protected void OnTriggerEnter2D(Collider2D Other)
    {
        var tryout =Other.GetComponent<HealthComponent>();
        if(tryout != null && _CanDamage)
        {
            float damagedealt = tryout.TakeDmg(_HPComp._Health);
            _HPComp.TakeDmg(damagedealt);
        }
    }
    
    public virtual void TakeDamage(float DMG)
    {
        _HPComp.TakeDmg(DMG);
    }
}