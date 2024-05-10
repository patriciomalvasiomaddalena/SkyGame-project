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

    private void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        RunLogic(this.transform);
    }
    public override Vector3 RunLogic(Transform _GunTransform)
    {
        mousePosition = MainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Rotation = mousePosition - _GunTransform.position;
        _AimMouseResult = Rotation;

        if (Input.GetMouseButtonDown(0))
        {
            PlayerShoot();
        }

        return Rotation;
    }

  

}
