using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Campaign_Input_Base :MonoBehaviour
{
    public abstract Vector3 InputMachine(Transform FleetTransform);
}
