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
        else
        {
            Debug.LogError("IATarget Is Null");
            return this.transform.position;
        }
        Vector3 Director = (_TargetPos.transform.position - this.transform.position).normalized * ModDist;
        return Director;
    }

    private void MoveTargetNode()
    {
        Debug.Log("movetarget");
        Vector3 NewPositionToMove = new Vector3(Random.Range(-6, 7), Random.Range(-3, 3));
        _TargetPos.transform.position = NewPositionToMove;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, _ArrivalDist);
    }

}
