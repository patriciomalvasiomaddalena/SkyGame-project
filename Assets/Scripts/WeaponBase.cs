using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponBase : ModuleBase
{
    [Header("Refs")]
    [SerializeField] AimBase _AimInput;
    [SerializeField] TestBullet _BulletType;

    [Header("Variables")]
    float Rotz;
    Vector3 Rotator;
    [SerializeField] string _FactoryString;
    [SerializeField] int _Bulletcount;
    [SerializeField] float _BulletFireSpeed;
    [SerializeField] float _RotationSpeed,_Recoil;
    [SerializeField] float _AmmoMag,_MaxAmmoMag,_AmmoRegenRate;
    [SerializeField] bool _FireBool;

    [Header("Npc config")]
    bool _IsNPC = false;
    bool _NPCDischarge;
    private void Start()
    {
        _AimInput.IsBeingUsed = true;
        _AimInput.PlayerShoot += StartFiring;
        _AimInput.PlayerStopShoot += StopFiring;

        if(_AimInput is IA_Aim_Turret)
        {
            _IsNPC = true;
            _AmmoRegenRate = _AmmoRegenRate * 1.2f;
        }
    }

    float _ammopulse;
    private void Update()
    {
        if (_AmmoMag < _MaxAmmoMag)
        {
            if (_ammopulse < _AmmoRegenRate)
            {
                _ammopulse = _ammopulse + (1 * Time.deltaTime);
            }
            else
            {
                _AmmoMag++;
                _ammopulse = 0;
            }
        }
        if (_FireBool && _IsNPC == false)
        {
            WeaponFire();
        }

        else if(_FireBool && (_AmmoMag == _MaxAmmoMag || _NPCDischarge == true)  && _IsNPC == true) // fire logic for AI
        {
            NPCWeaponFire();
        }
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

    float _WeaponPulse;
    protected void WeaponFire()
    {
            if (_WeaponPulse >= _BulletFireSpeed && _AmmoMag > 0) 
            {
                _AmmoMag--;
                float RecoilRand = Random.Range(-_Recoil, _Recoil);
                var Bullet = TestBulletFactory.InstanceDictionary[_BulletType].GetObjectFromPool(this.transform.position, this.transform.rotation);
                Bullet.ExtraDir(RecoilRand);
                Bullet.Fired();
                _WeaponPulse = 0;
                AudioManager.instance?.PlayMasterSfxAudio("ID_Shoot");
            }
            else
            {
                _WeaponPulse = _WeaponPulse + (1 * Time.deltaTime);
            }
    }

    protected void NPCWeaponFire()
    {
        _NPCDischarge = true;
        if (_WeaponPulse >= _BulletFireSpeed && _AmmoMag > 0)
        {   
            _AmmoMag--;
            float RecoilRand = Random.Range(-_Recoil, _Recoil);
            var Bullet = TestBulletFactory.InstanceDictionary[_BulletType].GetObjectFromPool(this.transform.position, this.transform.rotation);
            Bullet.Fired();
            Bullet.ExtraDir(RecoilRand);
            _WeaponPulse = 0;
        }
        else
        {
            _WeaponPulse = _WeaponPulse + (1 * Time.deltaTime);
        }

        if(_AmmoMag <= 1)
        {
            _NPCDischarge= false;
        }
    }

    private void StartFiring()
    {
        _FireBool = true;
    }

    private void StopFiring()
    {
        _FireBool = false;
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
        GetComponent<Renderer>().material.color = Color.black;
       this.enabled= false;
    }


}
