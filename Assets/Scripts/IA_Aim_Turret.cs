using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IA_Aim_Turret : AimBase
{
    public override event OnPlayerShooting PlayerShoot;
    public override event MobilePlayerStopShoot PlayerStopShoot;

    Vector3 DrawGizmoFrom, DrawGizmoTo;
    [SerializeField] float _ShootAngle, RateOfFire;
    [SerializeField] GameObject _playerShip;

    Vector3 Director;
    public override Vector3 RunLogic(Transform _GunTransform)
    {
        if(GameManager.Instance.PlayerShip!= null)
        {
            Director = (GameManager.Instance.PlayerShip.transform.position - _GunTransform.transform.position);
        }
        else
        {
            return Vector3.zero;
        }
        AiShoot(_GunTransform, Director);
        return Director;
    }

    float Pulse;
    private void AiShoot(Transform IATurret, Vector3 Director)
    {
        RaycastHit2D RayHit = Physics2D.Raycast(this.transform.position, Director, 50f);
        DrawGizmoFrom = this.transform.position;
        DrawGizmoTo = Director;

        if (Vector3.Angle(IATurret.transform.forward, Director) < _ShootAngle / 2)
        {
            if(RateOfFire >= Pulse)
            {
                Pulse += 1 * Time.deltaTime;
            }
            else
            {
                PlayerShoot();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(DrawGizmoFrom, DrawGizmoTo,Color.red);
    }
}
