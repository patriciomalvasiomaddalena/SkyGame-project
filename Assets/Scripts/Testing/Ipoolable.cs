using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ipoolable<T>
{
    public void TurnOn(T GenericScript);

    public void TurnOff(T GenericScript);

}
