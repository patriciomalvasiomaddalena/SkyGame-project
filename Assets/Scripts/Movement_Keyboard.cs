using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Keyboard : Move_Base
{
    Vector3 MovementAxis;
    public override Vector3 RunLogic()
    {
        MovementAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return MovementAxis;
    }
}
