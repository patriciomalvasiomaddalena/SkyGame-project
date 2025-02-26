using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Accelerometer : Move_Base
{
    [SerializeField] Vector2 _MovementInput;

    public override Vector3 RunLogic()
    {
       /*if(SystemInfo.supportsAccelerometer == false)
       {
            config_manager._Instance.SwitchControllers(ControllerType.Joystick);
            Debug.LogError("System Does not support Accelerometer");
            return Vector3.zero;
       }*/
        
        
            _MovementInput = new Vector2(Input.acceleration.x, Input.acceleration.y);
            return _MovementInput;
        
    }
}
