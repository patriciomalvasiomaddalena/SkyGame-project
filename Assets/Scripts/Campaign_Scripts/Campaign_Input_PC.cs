using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign_Input_PC : Campaign_Input_Base
{
    Vector3 target;
    public override Vector3 InputMachine(Transform FleetTransform)
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("schmoving");
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
        }
        return target;
    }
}
