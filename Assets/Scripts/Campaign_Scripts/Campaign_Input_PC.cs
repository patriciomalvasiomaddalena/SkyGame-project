using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign_Input_PC : Campaign_Input_Base
{
    Vector3 target;
    [SerializeField] LineRendererController _LRC;

    private void Start()
    {
        _LRC = GetComponentInParent<LineRendererController>();
        _LRC.Points[1] = this.transform.position;
    }
    public override Vector3 InputMachine(Transform FleetTransform)
    {
        if (Input.GetMouseButton(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            _LRC.Points[1] = target;
            return Vector3.zero;
        }
        _LRC.Points[1] = target;
        return target;

    }
}
