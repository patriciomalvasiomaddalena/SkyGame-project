using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aim_Mouse : AimBase
{
    //public class Aim_Joystick : aimbase
    //
    //update () runlogic()
    //runlogic()

    Vector3 mousePosition;

    public Vector2 _AimMouseResult;

    Camera MainCam;

    public override event OnPlayerShooting PlayerShoot;
    public override event MobilePlayerStopShoot PlayerStopShoot;
    public bool IsFiring;


    private void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (IsBeingUsed == false)
        {
            enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(IsFiring == false)
            {
                PlayerShoot();
            }
            IsFiring = true;   
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(IsFiring == true)
            {
                PlayerStopShoot();
            }
            IsFiring = false;
        }
    }

    public override Vector3 RunLogic(Transform _GunTransform)
    {
        mousePosition = MainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Rotation = mousePosition - _GunTransform.position;
        _AimMouseResult = Rotation;

        return Rotation;
    }

  

}
