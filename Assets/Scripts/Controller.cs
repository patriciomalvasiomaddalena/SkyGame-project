using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Vector3 _moveDir;

    public abstract Vector3 GetMovementInput();
}
