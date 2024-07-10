using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Bullet_Base : MonoBehaviour
{
    [SerializeField] int _Health,_MaxHealth;

    [SerializeField] float _DespawnTimer,_Speed;
    [SerializeField] string _PoolDictionaryKey;
    float _DespawnPulse;
    public PoolFabricator _BaseBulletPool;
    [SerializeField] bool _IsFired;

    private void Awake()
    {
        _Health = _MaxHealth;
        //_BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];
    }

    private void Start()
    {
        //_BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];

        _Health = _MaxHealth;
        _DespawnPulse = _DespawnTimer;
   
    }
    public void Fired(float FireStrenght)
    {
        _IsFired= true;

        _Speed = FireStrenght;

        if (_BaseBulletPool == null)
        {
            _BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];
        }
        if (_BaseBulletPool != null)
        {
            _BaseBulletPool.RemoveObjFromPool(this.gameObject);
        }

    }
    

    private void OnEnable()
    {
        _Health = _MaxHealth;
        _DespawnPulse = _DespawnTimer;
        _IsFired = false;
    }

    public void Update()
    {
        if (_BaseBulletPool == null)
        {
            //Debug.LogError("Bullet pool in null" + this.name);
            return;
        }

        if (_IsFired)
        {
            MovementLogic();
        }

        if (_DespawnPulse > 0) 
        {
            _DespawnPulse -= Time.deltaTime;
        }
        else
        {
            if (!_BaseBulletPool.Pool.Contains(this.gameObject))
            {
                TotalBulletDeath();
            }
            else
            {
                //Debug.LogError("Bullet Base is trying to access a pool that doesn´t have it: " + _PoolDictionaryKey);
            }
 
        }
    }
    private void MovementLogic()
    {
        this.transform.position += transform.up * _Speed*Time.deltaTime;
    }


    private void Resetvalues()
    {
        _Health = _MaxHealth;
        _DespawnPulse = _DespawnTimer;
        _IsFired = false;

    }

    private void TotalBulletDeath()
    {
        this.transform.position = _BaseBulletPool.transform.position;
        _BaseBulletPool.AddObjToPool(this.gameObject);
        Resetvalues();
        _IsFired = false;
        //AudioManager.instance?.PlayMasterSfxAudio("ID_Kaboom");
        gameObject.SetActive(false);
    }

}
