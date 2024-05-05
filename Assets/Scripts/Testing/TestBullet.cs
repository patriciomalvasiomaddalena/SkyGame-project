using System;
using System.Collections;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] float Health, MaxHealth;


    private void Awake()
    {

    }

    private void Update()
    {
        if(Health > 0)
        {
            Health= Health - (1*Time.deltaTime);
        }
        else
        {
            TestBulletFactory.Instance.ReturnObjectToPool(this);
            TurnOff(this);
        }
    }

    private void ResetVal()
    {
        Health = MaxHealth;
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