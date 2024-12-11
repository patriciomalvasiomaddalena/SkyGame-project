using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Explosive : TestBullet
{
    [SerializeField] float _radius;

    private void Awake()
    {
        _CanDamage = false;
        _Health = _MaxHealth;
    }
    private void Update()
    {
        if (_Health > 0)
        {
            _Health = _Health - (1 * Time.deltaTime);
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
        _Health = _MaxHealth;
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
        if (Other.gameObject.CompareTag("ShipPart") && _CanDamage == true && Other.gameObject.activeSelf == true)
        {
            AoEManager.AoECalculation(this.transform, _radius, _Health);
            _Health = -1;
        }
    }

}
