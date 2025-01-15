using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInputTouch : CamInput_base
{
    [SerializeField] private Camera _CampCam;

    Vector2 StartPos, dragStartPos, DragEndPos, Finger0Pos;
    //
    // TO DO: ADD A WAY TO UNLOCK FLEET SELECTION :)

    public override void RunLogic()
    {
        if(Input.touchCount== 1 && !CampaignManager.Instance.FleetSelected)
        {
            if(Input.GetTouch(0).phase==TouchPhase.Moved) 
            {
                Vector2 NewWorldPos = GetWorldPos();
                Vector2 Difference = NewWorldPos - StartPos;
                _CampCam.transform.Translate(-Difference);
            }
            StartPos = GetWorldPos();
        }
    }

    Vector2 GetWorldPos()
    {
        return _CampCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
