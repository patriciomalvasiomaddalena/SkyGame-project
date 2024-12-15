using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ProximityFuse : TestBullet
{
    [SerializeField] float _AoeDamage;
    float RadiusOfCheck;
    CircleCollider2D CircleCol;
    [SerializeField] HealthComponent _HPComponent;
    [SerializeField] Vfx_Exp_Script _ExplosionVfx;

    private void Awake()
    {
        _HPComponent = GetComponent<HealthComponent>();
        CircleCol= GetComponent<CircleCollider2D>();
        RadiusOfCheck = CircleCol.radius;

    }

    private void Start()
    {
        _HPComponent.SetHealth(_MaxHealth);
        _ExplosionVfx.SetParent(transform);
    }

    private void Update()
    {
        if (_HPComponent._Health > 0)
        {
            _HPComponent.TakeDmg(1 * Time.deltaTime);
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

    private new void OnTriggerEnter2D(Collider2D Other)
    {
        var EnemyHP = Other.GetComponent<HealthComponent>();

        if(EnemyHP!= null && _CanDamage)
        {
            _ExplosionVfx.SetParent(this.transform);
            _ExplosionVfx.PlayVFX();
            AoEManager.AoECalculation(transform, RadiusOfCheck, _AoeDamage);
            _HPComponent.TakeDmg(100);
        }

    }


    private List<TestBullet> AoEGetComponentsOfType(Transform PositionOfCheck, float AoERadius)
    {
        if (PositionOfCheck == null)
        {
            return null;
        }

        List<TestBullet> list = new List<TestBullet>();
        Collider2D[] listToFilter = Physics2D.OverlapCircleAll(PositionOfCheck.position, AoERadius);

        foreach (Collider2D col in listToFilter)
        {
            if (col.TryGetComponent(out TestBullet TestB))
            {
                list.Add(TestB);
                print(TestB.name);
            }
        }

        return list;
    }

    private void OnEnable()
    {
        ResetVal();
        _ExplosionVfx.ActivateGMOB();
        _ExplosionVfx.SetParent(transform);
    }
    protected override void ResetVal()
    {
        _HPComp.Revive();
        _CanDamage = false;
        _WindowDamage = 0.2f;

    }

}
