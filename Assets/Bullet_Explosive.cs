using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Explosive : TestBullet
{
    [SerializeField] float _radius;
    public HealthComponent _HealthComponent { get; private set; }
    [SerializeField] Vfx_Exp_Script _ExplosionVfx;

    private void Awake()
    {
        _HealthComponent = GetComponent<HealthComponent>();
        _CanDamage = false;
        _HealthComponent.SetHealth(_MaxHealth);
    }
    private void Start()
    {
        _ExplosionVfx.SetParent(this.transform);
    }

    private void Update()
    {

        if (_HealthComponent._Health > 0)
        {
            _HealthComponent.TakeDmg(1 * Time.deltaTime);
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
    public new void ExtraDir(float dir)
    {
        if (dir != 0)
        {
            this.transform.rotation *= Quaternion.Euler(0, 0, dir);
        }
    }

    public new void Fired()
    {
        this.transform.position += _speed * Time.deltaTime * transform.up;
    }
    private new void ResetVal()
    {
        _HealthComponent.Revive();
        _CanDamage = false;
        _WindowDamage = 0.2f;
    }

    public static void TurnOn(Bullet_Explosive b)
    {
            b.ResetVal();
            b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet_Explosive b)
    {
        b.gameObject.SetActive(false);
    }

    private new void OnTriggerEnter2D(Collider2D Other)
    {
        var EnemyHealth = Other.GetComponent<HealthComponent>();
        if (EnemyHealth != null && _CanDamage)
        {
            _ExplosionVfx.ActivateGMOB();
            _ExplosionVfx.SetParent(this.transform);
            _ExplosionVfx.PlayVFX();
            AoEManager.AoECalculation(this.transform, _radius, _HealthComponent._Health);
           _HealthComponent.TakeDmg(100);
        }
    }

    private void OnEnable()
    {
        ResetVal();
    }
}
