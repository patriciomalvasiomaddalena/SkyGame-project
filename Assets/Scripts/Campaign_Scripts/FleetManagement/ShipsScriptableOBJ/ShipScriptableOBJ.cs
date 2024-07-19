using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipScriptableOBJ : ScriptableObject
{
    public string ShipName;
    public GameObject ShipBlueprint, ShipObjectReference;
   public ShipScriptableOBJ(string NewName, GameObject NewShipBlueprint)
   {
        ShipName = NewName;
        ShipBlueprint = NewShipBlueprint;
   }
}
