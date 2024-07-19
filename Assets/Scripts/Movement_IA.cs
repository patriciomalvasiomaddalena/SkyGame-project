using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_IA : Move_Base
{
    [SerializeField] Transform _TargetPos;
    [SerializeField] float _MinDistance,_ArrivalDist;
    [SerializeField] float ModDist;

    float Distance;

    public override Vector3 RunLogic()
    {
        if(_TargetPos== null)
        {
            CreateMoveTarget();
        }

        if(_TargetPos!= null)
        {
            Distance = Vector3.Distance(_TargetPos.position, transform.position);

            if(Distance < _ArrivalDist)
            {
                ModDist = 0.9f;
            }
            else
            {
                ModDist = 1f;
            }

            if (Distance < _MinDistance) 
            {
                MoveTargetNode();
            }
        }
        Vector3 Director = (_TargetPos.transform.position - this.transform.position).normalized * ModDist;
        return Director;
    }

    private void CreateMoveTarget()
    {
        GameObject NewTarget = new GameObject();
        _TargetPos = NewTarget.transform;
        MoveTargetNode();
    }
    private void MoveTargetNode()
    {
        Vector3 NewPositionToMove = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        _TargetPos.transform.position= NewPositionToMove;

    }
}
