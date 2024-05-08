using System;
using System.Collections;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] float _Health, _MaxHealth;
    [SerializeField] LayerMask _HitMask;

    private void Update()
    {
        if(_Health > 0)
        {
            _Health= _Health - (1*Time.deltaTime);
        }
        else
        {
            TestBulletFactory.Instance.ReturnObjectToPool(this);
            TurnOff(this);
        }
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
}