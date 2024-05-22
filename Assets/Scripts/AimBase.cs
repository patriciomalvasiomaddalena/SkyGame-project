using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBase : MonoBehaviour
{
    public abstract Vector3 RunLogic(Transform _GunTransform);
    public bool IsBeingUsed;

    public delegate void OnPlayerShooting();
    public abstract event OnPlayerShooting PlayerShoot;

    public delegate void MobilePlayerStopShoot();
    public abstract event MobilePlayerStopShoot PlayerStopShoot;

}
