
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] CamInput_base CamInputKyM, CamInputTouch;

    [SerializeField] Vector3 _CampaignPosition, _FightPosition, _MaxCameraPosition;

    private void Start()
    {
        ScreenManager.Instance.SwitchedScene += positionCamera;
    }

    private void Update()
    {
        if (this.transform.position.x > _MaxCameraPosition.x)
        {
            this.transform.position = new Vector3(_MaxCameraPosition.x, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -_MaxCameraPosition.x)
        {
            this.transform.position = new Vector3(-_MaxCameraPosition.x, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.y > _MaxCameraPosition.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, _MaxCameraPosition.y, this.transform.position.z);
        }
        else if (this.transform.position.y < -_MaxCameraPosition.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, -_MaxCameraPosition.y, this.transform.position.z);
        }

    }


    private void LateUpdate()
    {
        if (ScreenManager.Instance.activeScreen == IScreenActive.ScreenCampaign)
        {
            if(Mathf.Abs(this.transform.position.x) > Mathf.Abs(_MaxCameraPosition.x))
            {
                return;
            }
            else if (Mathf.Abs(this.transform.position.y) > Mathf.Abs(_MaxCameraPosition.y))
            {
                return;
            }
            

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
            _CampaignPosition.z = 20;
        }
        else
        {
            print("fight camera");
            this.transform.position = _FightPosition;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, _MaxCameraPosition);
    }
}
