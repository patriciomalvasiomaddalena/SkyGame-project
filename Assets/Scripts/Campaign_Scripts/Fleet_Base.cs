using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fleet_Base : MonoBehaviour
{
    public List<ShipData> _FleetComposition;
    protected abstract void RunLogic();

    public abstract void DestroySelf();
}
