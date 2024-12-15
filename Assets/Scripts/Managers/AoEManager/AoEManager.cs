using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class AoEManager : MonoBehaviour
{
    private static  float _GizmoRadius;
    private static Vector3 _Gizmoposition;
    private static bool _DrawMeSomeGizmos;

    public static void AoECalculation(Transform PositionOfAoE,float AoERadius,float AoEDamage)
    {
        _DrawMeSomeGizmos = true;
        _Gizmoposition = PositionOfAoE.position;
        _GizmoRadius = AoERadius;

        Collider2D[] _HitColliders = Physics2D.OverlapCircleAll(PositionOfAoE.position, AoERadius);
        DebugCollision(_HitColliders);

        for(int i = 0; i < _HitColliders.Length;i++)
        {
            if (_HitColliders[i].TryGetComponent(out HealthComponent HPComp))
            {
                HPComp.TakeDmg(AoEDamage);
            }
        }

    }

    private static void DebugCollision(Collider2D[]ColArray)
    {
        Debug.Log("DebugCol " + ColArray.ToString());
    }

    private void OnDrawGizmos()
    {
        if (!_DrawMeSomeGizmos) return;

        Gizmos.DrawWireSphere(_Gizmoposition, _GizmoRadius);
    }
}
