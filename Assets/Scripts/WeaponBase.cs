using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponBase : ModuleBase
{
    [Header("Refs")]
    [SerializeField] AimBase _AimInput;
    [SerializeField] GameObject _BulletPrefab;

    [Header("Variables")]
    float Rotz;
    Vector3 Rotator;
    [SerializeField] string _FactoryString;
    [SerializeField] int _Bulletcount;
    [SerializeField] float _FireStrenght;
    [SerializeField] float _RotationSpeed;

    private void Start()
    {
        _AimInput.PlayerShoot += WeaponFire;
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
            var Bullet = TestBulletFactory.Instance.GetObjectFromPool(this.transform.position,this.transform.rotation);
            Bullet.Fired();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);
            GetComponentInParent<Hull_Piece>().ModuleAttach(this);
        }
    }

    public override void DisabledModule()
    {
       this.enabled= false;
    }
}
