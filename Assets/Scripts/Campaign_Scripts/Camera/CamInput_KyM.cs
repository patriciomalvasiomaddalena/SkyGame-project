using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInput_KyM : CamInput_base
{
    private Vector3 Origin;
    private Vector3 Difference;

    [SerializeField] Camera _CampaignCamera;

    [SerializeField] bool DragCamera;

    public override void RunLogic()
    {

        if (Input.GetMouseButton(0))
        {
            //obtain difference between mouseposition and camera
            Difference = _CampaignCamera.ScreenToWorldPoint(Input.mousePosition) - _CampaignCamera.transform.position;

            if (!DragCamera)
            {
                // lock origin point
                DragCamera = true; 
                Origin = _CampaignCamera.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        else
        {
            DragCamera = false;
        }

        if(DragCamera)
        {
            //move camera
            _CampaignCamera.transform.position = Origin - Difference;
        }
    }

}
