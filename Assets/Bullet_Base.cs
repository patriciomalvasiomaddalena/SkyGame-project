using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Bullet_Base : MonoBehaviour
{
    [SerializeField] int _Health,_MaxHealth;

    [SerializeField] float _DespawnTimer;
    [SerializeField] string _PoolDictionaryKey;
    float _DespawnPulse;
    public PoolFabricator _BaseBulletPool;

    Rigidbody2D _RB2D;
    private void Awake()
    {
        _RB2D= GetComponent<Rigidbody2D>();
        _Health = _MaxHealth;
        _BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];
    }

    private void Start()
    {
        _BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];
        this.gameObject.SetActive(false);
    }
    public void Fired(Vector2 ProjectileVel, Quaternion CannonRotation)
    {

        _RB2D.AddForce(ProjectileVel, ForceMode2D.Impulse);

        this.transform.rotation = CannonRotation;

    }
    

    private void OnEnable()
    {
        _BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];

        _Health = _MaxHealth;
        _DespawnPulse = _DespawnTimer;

        if(_BaseBulletPool == null)
        {
            _BaseBulletPool = PoolManager.Instance.PoolDictionary[_PoolDictionaryKey];
        }
        if(_BaseBulletPool != null )
        {
            _BaseBulletPool.RemoveObjFromPool(this.gameObject);
        }

    }

    public void Update()
    {
        if(_BaseBulletPool == null)
        {
            //Debug.LogError("Bullet pool in null" + this.name);
            return;
        }

        if(_DespawnPulse > 0) 
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
                Debug.LogError("Bullet Base is trying to access a pool that doesn´t have it: " + _PoolDictionaryKey);
            }
 
        }

    }

    private void TotalBulletDeath()
    {
        this.transform.position = _BaseBulletPool.transform.position;
        _BaseBulletPool.AddObjToPool(this.gameObject);
        gameObject.SetActive(false);
    }

}
