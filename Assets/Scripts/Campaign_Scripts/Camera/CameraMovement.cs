
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] CamInput_base CamInputKyM, CamInputTouch;

    private void LateUpdate()
    {
      switch (config_manager._Instance.CurrentController)
        {
            case ControllerType.KyM:
                CamInputKyM.RunLogic();

                break;

            case ControllerType.Gyro_Touch:

            case ControllerType.Joystick:
                CamInputTouch.RunLogic();
            break;

        }

    }
}
