using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ModuleBase
{
    [SerializeField] AimBase _AimInput;

    [SerializeField] GameObject _BulletPrefab;
    float Rotz;
    Vector3 Rotator;

    [SerializeField] float _RotationSpeed;
    [SerializeField] string _PoolNameFabricator ="turretBoolet";
    [SerializeField] PoolFabricator _BulletPool;

    private void Start()
    {
        PoolManager.PoolType bulletpool = new PoolManager.PoolType();
        bulletpool.ObjectBlueprint = _BulletPrefab;
        bulletpool.SizeofPool = 5;
        bulletpool.name = _PoolNameFabricator;
        PoolManager.Instance.AddNewPool(bulletpool);
        _AimInput.PlayerShoot += WeaponFire;

        _BulletPool = PoolManager.Instance.PoolDictionary[_PoolNameFabricator];
    }

    private void FixedUpdate()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {
        Rotator = _AimInput.RunLogic(this.transform);
        
        Rotz = Mathf.Atan2(-Rotator.x, Rotator.y) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,Quaternion.Euler(0,0,Rotz),_RotationSpeed * Time.fixedDeltaTime);

    }

    protected void WeaponFire()
    {
        if(_BulletPool != null)
        {
            GameObject bullet = _BulletPool.GrabPooledItem();
            bullet.transform.position = this.transform.position;
        }
        else
        {
            _BulletPool = PoolManager.Instance.PoolDictionary[_PoolNameFabricator];
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);

        }
    }

}
