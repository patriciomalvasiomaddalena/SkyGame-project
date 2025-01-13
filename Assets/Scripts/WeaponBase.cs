using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponBase : ModuleBase
{
    [Header("Refs")]
    [SerializeField] AimBase _AimInput; // this is the variable used to set current input
    [SerializeField] AimBase _AimKyM, _AimJoystick, _AimGyro, _AimNpC; // this is used to know what input we have available
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
    [SerializeField] ControllerType _CurrentController;

    [Header("Npc config")]
    bool _IsNPC = false;
    bool _NPCDischarge;
    private void Start()
    {
        _CurrentController = config_manager._Instance.CurrentController;

        if(_AimInput is IA_Aim_Turret) // this turret is an IA, so we skip the other checks
        {
            _IsNPC = true;
            _AmmoRegenRate = _AmmoRegenRate * 1.2f;
        }
        else
        {
            SwitchController();
        }
        _AimInput.IsBeingUsed = true;
        _AimInput.PlayerShoot += StartFiring;
        _AimInput.PlayerStopShoot += StopFiring;
    }

    private void SwitchController()
    {
        _CurrentController = config_manager._Instance.CurrentController;

        switch( _CurrentController )
        {
            case ControllerType.KyM:
                _AimInput = _AimKyM;
                break;

            case ControllerType.Gyro_Touch:
                _AimInput = config_manager._Instance.JoystickAim.GetComponent<Aim_Turret_Joystick>();

            break;
            case ControllerType.Joystick:
                _AimInput = config_manager._Instance.JoystickAim.GetComponent<Aim_Turret_Joystick>();

                break;
        }
        _AimInput.gameObject.SetActive(true);
        _AimInput.enabled = true;
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
