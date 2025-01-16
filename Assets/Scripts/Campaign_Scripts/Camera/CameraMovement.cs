
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] CamInput_base CamInputKyM, CamInputTouch;

    [SerializeField] Vector3 _CampaignPosition, _FightPosition;

    private void Start()
    {
        ScreenManager.Instance.SwitchedScene += positionCamera;
    }

    private void LateUpdate()
    {
        if(ScreenManager.Instance.activeScreen == IScreenActive.ScreenCampaign)
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

            _CampaignPosition = this.transform.position;
        }
        else
        {
            positionCamera();
        }
    }

    private void positionCamera()
    {
        if(ScreenManager.Instance.activeScreen == IScreenActive.ScreenCampaign)
        {
            print("world camera");
            this.transform.position = _CampaignPosition;
        }
        else
        {
            print("fight camera");
            this.transform.position = _FightPosition;
        }
    }
}
